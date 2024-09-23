using Api.Configurations;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAppConections(builder.Configuration)
    .AddUseCases()
    .AddAndConfigureControllers();

var app = builder.Build();

app.Services.CreateScope().ServiceProvider.GetRequiredService<DesafioDbContext>().Database.Migrate();

app.UseDocumentation();
app.MapControllers();


app.Run();

public partial class Program { }