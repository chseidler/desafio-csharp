using Domain.Enum;
using MediatR;

namespace Application.UseCases.Payment.MakePayment;

public record MakePaymentInput(Guid OrderId, PaymentMethodEnum Method) : IRequest<MakePaymentOutput>;
