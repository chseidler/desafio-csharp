namespace Application.UseCases.Payment.MakePayment;

public class MakePayment : IMakePayment
{
    public Task<MakePaymentOutput> Handle(MakePaymentInput request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
