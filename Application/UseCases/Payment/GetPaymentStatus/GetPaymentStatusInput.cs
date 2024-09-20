using MediatR;

namespace Application.UseCases.Payment.GetPaymentStatus;

public record GetPaymentStatusInput(Guid PaymentId) : IRequest<GetPaymentStatusOutput>;
