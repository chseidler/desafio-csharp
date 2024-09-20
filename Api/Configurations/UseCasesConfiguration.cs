using Application.Interfaces;
using Application.UseCases.Item;
using Domain.Repository;
using Infrastructure.Email;
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
        services.AddServices();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IItemRepository, ItemRepository>();
        services.AddTransient<IOrderRepository, OrderRepository>();
        services.AddTransient<IPaymentRepository, PaymentRepository>();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IEmailService, EmailService>();

        return services;
    }
}