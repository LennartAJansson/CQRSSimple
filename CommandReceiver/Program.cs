using CommandReceiver.Configuration;
using CommandReceiver.Services;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddConfiguration(builder.Configuration);
builder.Services.AddServices();
builder.Services.AddGrpc();

WebApplication? app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();