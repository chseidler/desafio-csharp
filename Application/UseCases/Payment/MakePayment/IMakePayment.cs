using MediatR;

namespace Application.UseCases.Payment.MakePayment;

public interface IMakePayment : IRequestHandler<MakePaymentInput, MakePaymentOutput>
{
}
