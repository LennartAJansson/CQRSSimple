using System.Reflection;
using System.Text.Json;

using AllInOne.Configuration;
using AllInOne.Contracts;
using AllInOne.Data;
using AllInOne.Mappers;
using AllInOne.Messengers;
using AllInOne.Model;
using AllInOne.Operations;
using AllInOne.Services;

using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder);

WebApplication? app = builder.Build();

UpdateDatabase(app);

Configure(app);

app.Run();


//Local methods:

static void ConfigureServices(WebApplicationBuilder builder)
{
    // Add services to the container.
    builder.Services.AddHostedService<OperationsHandler>();

    builder.Services.Configure<ConnectionStrings>(settings => builder.Configuration.GetSection("ConnectionStrings").Bind(settings));
    builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
    builder.Services.AddAutoMapper(c => c.AddProfile<ModelMapper>(), typeof(ModelMapper).Assembly);
    builder.Services.AddSingleton<IOperationsMessenger, OperationsMessenger>();
    builder.Services.AddTransient<ICommandService, CommandService>();
    builder.Services.AddTransient<IQueryService, QueryService>();
    builder.Services.AddDbContext<WeatherForecastsContext>(options => options
        .UseSqlServer(builder.Configuration.GetConnectionString("WeatherForecastsDb")), ServiceLifetime.Transient, ServiceLifetime.Transient);

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options => options
        .SwaggerDoc("v1", new OpenApiInfo { Title = "CQRS", Version = "v1" }));
}

static void Configure(WebApplication app)
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "CQRS v1");
            c.ConfigObject.AdditionalItems.Add("syntaxHighlight", false);
            //Turns off syntax highlight which causing performance issues...
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        });
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
}


void UpdateDatabase(WebApplication app)
{
    using (IServiceScope scope = app.Services.CreateScope())
    {
        WeatherForecastsContext context = scope.ServiceProvider.GetRequiredService<WeatherForecastsContext>();
        IMapper mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
            if (context.WeatherForecasts != null && context.Operations != null)
            {
                if (!context.WeatherForecasts.Any())
                {
                    JsonSerializerOptions? jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    string? json = File.ReadAllText("seeddata.json");
                    IEnumerable<ReadWeatherForecastResponse>? forecasts = JsonSerializer.Deserialize<IEnumerable<ReadWeatherForecastResponse>>(json, jsonOptions);
                    json = File.ReadAllText("seedoperations.json");
                    IEnumerable<ReadOperationResponse>? operations = JsonSerializer.Deserialize<IEnumerable<ReadOperationResponse>>(json, jsonOptions);
                    if (forecasts != null && operations != null)
                    {
                        context.WeatherForecasts.AddRange(forecasts.Select(f => mapper.Map<WeatherForecast>(f)));
                        context.SaveChanges();
                        context.Operations.AddRange(operations.Select(o => mapper.Map<Operation>(o)));
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}