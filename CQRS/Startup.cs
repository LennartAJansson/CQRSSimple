namespace CQRS
{
    using System.Linq;
    using System.Reflection;

    using CQRS.Configuration;
    using CQRS.Data;
    using CQRS.Operations;
    using CQRS.Queue;
    using CQRS.Services;

    using MediatR;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;

    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHostedService<OperationsHandler>();
            services.AddConfiguration(Configuration);
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddSingleton<IOperationsMessenger, OperationsMessenger>();
            services.AddTransient<ICommandService, CommandService>();
            services.AddTransient<IQueryService, QueryService>();
            services.AddDbContext<WeatherForecastsContext>(options => options
                .UseSqlServer(Configuration.GetConnectionString("WeatherForecastsDb")), ServiceLifetime.Transient, ServiceLifetime.Transient);
            services.AddControllers();
            services.AddSwaggerGen(options => options
                .SwaggerDoc("v1", new OpenApiInfo { Title = "CQRS", Version = "v1" }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (IServiceScope scope = app.ApplicationServices.CreateScope())
            {
                WeatherForecastsContext context = scope.ServiceProvider.GetRequiredService<WeatherForecastsContext>();
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CQRS v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints
                .MapControllers());
        }
    }
}
