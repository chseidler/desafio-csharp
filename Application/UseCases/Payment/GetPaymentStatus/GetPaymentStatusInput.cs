using FluentResults;
using MediatR;

namespace Application.UseCases.Payment.GetPaymentStatus;

public record GetPaymentStatusInput(Guid PaymentId) : IRequest<Result<GetPaymentStatusOutput>>;
