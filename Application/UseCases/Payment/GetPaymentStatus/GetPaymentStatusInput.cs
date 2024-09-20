using FluentResults;
using MediatR;

namespace Application.UseCases.Payment.GetPaymentStatus;

public record GetPaymentStatusInput(Guid OrderId) : IRequest<Result<GetPaymentStatusOutput>>;
