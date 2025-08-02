using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure;

namespace PalMazadStore.Migrations;
public class AppDbContext : BaseDbContext
{
    public AppDbContext(DbContextOptions<BaseDbContext> options) : base(options)
    {
    }

    public DbSet<UserEntity> Users { get; set; } = default!;
    public DbSet<ProductEntity> Products { get; set; } = default!;
    public DbSet<CategoryEntity> Categories { get; set; } = default!;
    public DbSet<OrderEntity> Orders { get; set; } = default!;
    public DbSet<OrderItemEntity> OrderItemEntity { get; set; } = default!;
    public DbSet<PaymentEntity> Payments { get; set; } = default!;
    public DbSet<CommissionSettingEntity> CommissionSettings { get; set; } = default!;
    public DbSet<CartItemEntity> CartItemEntity { get; set; } = default!;
    public DbSet<CartEntity> CartEntity { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // UserEntity
        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.HasKey(u => u.Id);

            entity.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(u => u.PhoneNumber)
                .HasMaxLength(20);

            entity.Property(u => u.PasswordHash)
                .IsRequired();

            entity.Property(u => u.IsSeller)
                .IsRequired();

            entity.Property(u => u.IsVerifiedSeller)
                .IsRequired();

            entity.Property(u => u.IsIdentityHidden)
                .IsRequired();

            entity.Property(u => u.CreatedOn)
                .HasDefaultValueSql("GETUTCDATE()");

            // Relationships
            entity.HasMany(u => u.Products)
                .WithOne(p => p.Seller)
                .HasForeignKey(p => p.SellerId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(u => u.OrdersAsBuyer)
                .WithOne(o => o.Buyer)
                .HasForeignKey(o => o.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // CategoryEntity
        modelBuilder.Entity<CategoryEntity>(entity =>
        {
            entity.HasKey(c => c.Id);

            entity.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // ProductEntity
        modelBuilder.Entity<ProductEntity>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(p => p.Description)
                .HasMaxLength(2000);

            entity.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            // Store enum as string (optional, comment if you want int)
            entity.Property(p => p.Condition)
                .HasConversion<string>()
                .IsRequired();

            entity.Property(p => p.IsPublished)
                .IsRequired()
                .HasDefaultValue(true);

            entity.Property(p => p.IsHiddenSellerInfo)
                .IsRequired()
                .HasDefaultValue(false);

            entity.Property(u => u.CreatedOn)
                .HasDefaultValueSql("GETUTCDATE()");

        });

        // OrderEntity
        modelBuilder.Entity<OrderEntity>(entity =>
        {
            entity.HasKey(o => o.Id);

            entity.Property(o => o.TotalAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            entity.Property(o => o.CommissionAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            entity.Property(o => o.Status)
                .HasConversion<string>()
                .IsRequired();

            entity.Property(o => o.CreatedOn)
                .HasDefaultValueSql("GETUTCDATE()");

            // Relationship to Payment one-to-one
            entity.HasOne(o => o.Payment)
                .WithOne(p => p.Order)
                .HasForeignKey<PaymentEntity>(p => p.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(o => o.Buyer)
                .WithMany(u => u.OrdersAsBuyer)
                .HasForeignKey(o => o.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // PaymentEntity
        modelBuilder.Entity<PaymentEntity>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            entity.Property(p => p.PaymentMethod)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(p => p.IsSuccessful)
                .IsRequired();

            entity.Property(p => p.CreatedOn)
                .HasDefaultValueSql("GETUTCDATE()");
        });

        // CommissionSettingEntity
        modelBuilder.Entity<CommissionSettingEntity>(entity =>
        {
            entity.HasKey(c => c.Id);

            entity.Property(c => c.Percentage)
                .IsRequired()
                .HasColumnType("decimal(5,4)"); // e.g., 0.1000 = 10%

            entity.Property(c => c.ActiveFromDate)
                .IsRequired();
        });

        // CartEntity:
        modelBuilder.Entity<CartEntity>()
            .HasOne(c => c.User)
            .WithOne(u => u.Cart)
            .HasForeignKey<CartEntity>(c => c.UserId);

        modelBuilder.Entity<CartItemEntity>()
            .HasOne(ci => ci.Cart)
            .WithMany(c => c.Items)
            .HasForeignKey(ci => ci.CartId);

        modelBuilder.Entity<CartItemEntity>()
            .HasOne(ci => ci.Product)
            .WithMany()
            .HasForeignKey(ci => ci.ProductId)
             .OnDelete(DeleteBehavior.NoAction);

        // OrderItemEntity
        modelBuilder.Entity<OrderEntity>()
            .HasMany(o => o.Items)
            .WithOne(i => i.Order)
            .HasForeignKey(i => i.OrderId)
            .OnDelete(DeleteBehavior.Cascade); // If an order is deleted, delete its items

        modelBuilder.Entity<OrderItemEntity>()
            .HasOne(i => i.Product)
            .WithMany() // If you don't need navigation from Product to OrderItems
            .HasForeignKey(i => i.ProductId);
    }



}