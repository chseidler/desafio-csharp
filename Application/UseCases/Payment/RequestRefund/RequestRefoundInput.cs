using MediatR;

namespace Application.UseCases.Payment.RequestRefund;

public record RequestRefoundInput(Guid Id) : IRequest<RequestRefoundOutput>;
