using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure;

namespace PalMazadStore.Migrations;
public class AppDbContext : BaseDbContext
{
    public AppDbContext(DbContextOptions<BaseDbContext> options) : base(options)
    {
    }

    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Soft delete filters
        modelBuilder.Entity<ProductEntity>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<OrderEntity>().HasQueryFilter(e => !e.IsDeleted);

        // Product config
        modelBuilder.Entity<ProductEntity>(entity =>
        {
            entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Description).HasMaxLength(500);
            entity.Property(p => p.ProductQR).IsRequired().HasMaxLength(200);
            entity.Property(p => p.Price).HasColumnType("decimal(18,2)");
        });

        // Order config
        modelBuilder.Entity<OrderEntity>(entity =>
        {
            entity.HasOne(o => o.Product)
                  .WithMany() // or WithMany(p => p.Orders) if added
                  .HasForeignKey(o => o.ProductId);

            entity.Property(o => o.BuyerName).IsRequired().HasMaxLength(100);
            entity.Property(o => o.BuyerPhone).IsRequired().HasMaxLength(15);
            entity.Property(o => o.Status).HasConversion<int>(); // Store enum as int
        });
    }


}