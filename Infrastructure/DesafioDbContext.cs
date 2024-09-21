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

        // Configuração para ItemDomain
        modelBuilder.Entity<ItemDomain>().HasKey(i => i.Id);

        // Configuração para OrderDomain
        modelBuilder.Entity<OrderDomain>().HasKey(o => o.Id);

        // Configuração para OrderItem
        modelBuilder.Entity<OrderItemDomain>()
            .HasKey(oi => new { oi.OrderId, oi.ItemId }); // Chave composta

        modelBuilder.Entity<OrderItemDomain>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.Items)
            .HasForeignKey(oi => oi.OrderId);

        modelBuilder.Entity<OrderItemDomain>()
            .HasOne(oi => oi.Item)
            .WithMany()
            .HasForeignKey(oi => oi.ItemId);
    }
}
