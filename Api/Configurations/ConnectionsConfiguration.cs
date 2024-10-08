using Infrastructure;
using Microsoft.EntityFrameworkCore;

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
        services.AddDbContext<DesafioDbContext>(opt =>
            opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        return services;
    }
}