using FluentResults;

namespace Application.UseCases.Payment.GetPaymentStatus;

public class GetPaymentStatus : IGetPaymentStatus
{
    public async Task<Result<GetPaymentStatusOutput>> Handle(GetPaymentStatusInput request, CancellationToken cancellationToken)
    {
        try
        {
            throw new NotImplementedException();
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}
