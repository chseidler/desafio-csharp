using Domain.Enum;

namespace Application.UseCases.Payment.PaymentMethod;

public class PaymentFactory
{
    public static IPayment CreatePayment(PaymentMethodEnum method)
    {
        return method switch
        {
            PaymentMethodEnum.Pix => new PixPayment(),
            PaymentMethodEnum.Credito => new CreditoPayment(),
            PaymentMethodEnum.Debito => new DebitoPayment(),
            PaymentMethodEnum.Boleto => new BoletoPayment(),
            _ => throw new ArgumentException("Invalid payment method")
        };
    }
}
