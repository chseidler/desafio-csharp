using MediatR;

namespace Application.UseCases.Payment.RequestRefund;

public interface IRequestRefound : IRequestHandler<RequestRefoundInput, RequestRefoundOutput>
{
}
