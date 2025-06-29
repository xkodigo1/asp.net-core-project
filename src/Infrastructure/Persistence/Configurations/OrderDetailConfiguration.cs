using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.ToTable("OrderDetails");

        builder.HasKey(od => od.Id);

        builder.Property(od => od.Quantity)
            .IsRequired();

        builder.Property(od => od.UnitPrice)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(od => od.Discount)
            .HasPrecision(18, 2);

        builder.Property(od => od.Total)
            .HasPrecision(18, 2)
            .IsRequired();

        // Relaciones
        builder.HasOne(od => od.ServiceOrder)
            .WithMany(so => so.OrderDetails)
            .HasForeignKey(od => od.ServiceOrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(od => od.Spare)
            .WithMany(s => s.OrderDetails)
            .HasForeignKey(od => od.SpareId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(od => !od.IsDeleted);
    }
} 