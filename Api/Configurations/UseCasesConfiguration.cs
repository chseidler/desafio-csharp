using Application.UseCases.Item;
using Domain.Repository;
using Infrastructure.Repositories;

namespace Api.Configurations;

public static class UseCasesConfiguration
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(typeof(ListItems).Assembly);
        });
        services.AddRepositories();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IItemRepository, ItemRepository>();
        services.AddTransient<IOrderRepository, OrderRepository>();
        services.AddTransient<IPaymentRepository, PaymentRepository>();

        return services;
    }
}