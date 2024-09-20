using MediatR;

namespace Application.UseCases.Payment.RequestRefund;

public record RequestRefundInput(Guid OrderId) : IRequest<RequestRefundOutput>;
