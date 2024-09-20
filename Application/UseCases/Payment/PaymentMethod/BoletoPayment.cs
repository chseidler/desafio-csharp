using Application.Interfaces;
using Domain.Entity;
using Polly;

namespace Application.UseCases.Payment.PaymentMethod;

public class BoletoPayment : IPayment
{
    private readonly IPaymentGateway _paymentGateway;

    public BoletoPayment(IPaymentGateway paymentGateway)
    {
        _paymentGateway = paymentGateway;
    }

    public async Task<(bool IsSuccessful, decimal FinalAmount)> ProcessPayment(OrderDomain order)
    {
        var retryPolicy = Policy
            .Handle<Exception>()
            .RetryAsync(3);

        var result = await retryPolicy.ExecuteAsync(async () =>
        {
            bool success = await _paymentGateway.ProcessPaymentAsync(order.Total);
            return success;
        });

        return (result, order.Total);
    }

    public bool CanRefund() => false;

    public Task Refund(Guid paymentId)
    {
        throw new InvalidOperationException("Cant`t refund Boleto.");
    }
}
