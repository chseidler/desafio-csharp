using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class DesafioDbContext : DbContext
{
    public DbSet<ItemDomain> Items => Set<ItemDomain>();
    public DbSet<OrderDomain> Orders => Set<OrderDomain>();
    public DbSet<PaymentDomain> Payments => Set<PaymentDomain>();

    public DesafioDbContext(DbContextOptions<DesafioDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        //builder.ApplyConfiguration();
    }
}
