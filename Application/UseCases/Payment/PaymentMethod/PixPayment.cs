using Application.Interfaces;
using Domain.Entity;
using Polly;

namespace Application.UseCases.Payment.PaymentMethod;

public class PixPayment : IPayment
{
    private readonly IPaymentGateway _paymentGateway;

    public PixPayment(IPaymentGateway paymentGateway)
    {
        _paymentGateway = paymentGateway;
    }

    public async Task<(bool IsSuccessful, decimal FinalAmount)> ProcessPayment(OrderDomain order)
    {
        var pixValue = order.Total * 0.95m;

        var retryPolicy = Policy
            .Handle<Exception>()
            .RetryAsync(3);

        var result = await retryPolicy.ExecuteAsync(async () =>
        {
            bool success = await _paymentGateway.ProcessPaymentAsync(pixValue);
            return success;
        });

        return (result, pixValue);
    }

    public bool CanRefund() => false;

    public Task Refund(Guid paymentId)
    {
        throw new InvalidOperationException("Cant`t refund Pix.");
    }
}
