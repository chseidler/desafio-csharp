using Domain.Repository;
using FluentResults;

namespace Application.UseCases.Payment.GetPaymentStatus;

public class GetPaymentStatus : IGetPaymentStatus
{
    private readonly IPaymentRepository _paymentRepository;

    public GetPaymentStatus(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<Result<GetPaymentStatusOutput>> Handle(GetPaymentStatusInput request, CancellationToken cancellationToken)
    {
        try
        {
            var payment = await _paymentRepository.GetByOrderIdAsync(request.OrderId, cancellationToken);

            if (payment is null)
                return Result.Fail($"Payment with OrderId {request.OrderId} not found.");

            return Result.Ok(new GetPaymentStatusOutput(payment.Status));
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}
