using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class DesafioDbContext : DbContext
{
    public DesafioDbContext(DbContextOptions<DesafioDbContext> options) : base(options) { }

    public DbSet<ItemDomain> Items => Set<ItemDomain>();
    public DbSet<OrderDomain> Orders => Set<OrderDomain>();
    public DbSet<PaymentDomain> Payments => Set<PaymentDomain>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ItemDomain>().HasKey(i => i.Id);

        modelBuilder.Entity<OrderDomain>()
            .HasMany(o => o.Items)
            .WithOne()
            .HasForeignKey(oi => oi.OrderId);

        modelBuilder.Entity<OrderItemDomain>()
            .HasKey(oi => new { oi.OrderId, oi.ItemId });

        modelBuilder.Entity<OrderItemDomain>()
            .HasOne(oi => oi.Item)
            .WithMany();
    }
}
