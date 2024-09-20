
namespace Application.UseCases.Payment.RequestRefund;

public class RequestRefound : IRequestRefound
{
    public Task<RequestRefoundOutput> Handle(RequestRefoundInput request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
