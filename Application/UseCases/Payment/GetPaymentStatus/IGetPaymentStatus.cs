using MediatR;

namespace Application.UseCases.Payment.GetPaymentStatus;

public interface IGetPaymentStatus : IRequestHandler<GetPaymentStatusInput, GetPaymentStatusOutput>
{
}
