namespace Application.UseCases.Payment.GetPaymentStatus;

public class GetPaymentStatus : IGetPaymentStatus
{
    public Task<GetPaymentStatusOutput> Handle(GetPaymentStatusInput request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
