using FluentResults;
using MediatR;

namespace Application.UseCases.Payment.GetPaymentStatus;

public interface IGetPaymentStatus : IRequestHandler<GetPaymentStatusInput, Result<GetPaymentStatusOutput>>
{
}
