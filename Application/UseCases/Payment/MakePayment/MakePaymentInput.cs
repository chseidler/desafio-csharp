using Domain.Enum;
using FluentResults;
using MediatR;

namespace Application.UseCases.Payment.MakePayment;

public record MakePaymentInput(Guid OrderId, PaymentMethodEnum Method) : IRequest<Result<MakePaymentOutput>>;
