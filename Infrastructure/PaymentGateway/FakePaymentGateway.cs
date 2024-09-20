using Application.Interfaces;

namespace Infrastructure.PaymentGateway;

public class FakePaymentGateway : IPaymentGateway
{
    private readonly Random _random = new Random();

    public async Task<bool> ProcessPaymentAsync(decimal amount)
    {
        await Task.Delay(_random.Next(50, 250));

        return _random.Next(0, 2) == 0;
    }

    public async Task RefundPaymentAsync(Guid paymentId)
    {
        await Task.Delay(_random.Next(50, 250));
    }
}
