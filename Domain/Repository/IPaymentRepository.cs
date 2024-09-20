using Domain.Entity;

namespace Domain.Repository;

public interface IPaymentRepository
{
    Task SaveAsync(PaymentDomain payment, CancellationToken cancellationToken);
}
