using Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddUseCases()
    .AddAndConfigureControllers();

var app = builder.Build();
app.UseDocumentation();
app.MapControllers();

app.Run();