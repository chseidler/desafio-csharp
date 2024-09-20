using FluentResults;
using MediatR;

namespace Application.UseCases.Payment.RequestRefund;

public interface IRequestRefund : IRequestHandler<RequestRefundInput, Result<RequestRefundOutput>>
{
}
