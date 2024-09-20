using Application.Interfaces;
using Domain.Enum;

namespace Application.UseCases.Payment.PaymentMethod;

public class PaymentFactory
{
    public static IPayment CreatePayment(PaymentMethodEnum method, IPaymentGateway paymentGateway)
    {
        return method switch
        {
            PaymentMethodEnum.Pix => new PixPayment(paymentGateway),
            PaymentMethodEnum.Credito => new CreditoPayment(paymentGateway),
            PaymentMethodEnum.Debito => new DebitoPayment(paymentGateway),
            PaymentMethodEnum.Boleto => new BoletoPayment(paymentGateway),
            _ => throw new ArgumentException("Invalid payment method")
        };
    }
}
