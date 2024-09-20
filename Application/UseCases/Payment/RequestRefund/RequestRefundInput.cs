using FluentResults;
using MediatR;

namespace Application.UseCases.Payment.RequestRefund;

public record RequestRefundInput(Guid OrderId) : IRequest<Result<RequestRefundOutput>>;
