using Domain.Entity;
using Domain.Repository;

namespace Infrastructure.Repositories;

public class PaymentRepository : IPaymentRepository
{
    public Task SaveAsync(PaymentDomain payment, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
