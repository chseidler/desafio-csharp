using Domain.Entity;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly DesafioDbContext _dbContext;

    public PaymentRepository(DesafioDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PaymentDomain> GetByOrderIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Payments
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.OrderId == id, cancellationToken);
    }

    public async Task SaveAsync(PaymentDomain payment, CancellationToken cancellationToken)
    {
        await _dbContext.Payments.AddAsync(payment, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
