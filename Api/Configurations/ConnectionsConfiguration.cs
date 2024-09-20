using Microsoft.EntityFrameworkCore;
using Infrastructure;

namespace Api.Configurations;

public static class ConnectionsConfiguration
{
    public static IServiceCollection AddAppConections(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbConnection(configuration);

        return services;
    }

    private static IServiceCollection AddDbConnection(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DesafioDb");
        services.AddDbContext<DesafioDbContext>(); // TODO

        return services;
    }
}