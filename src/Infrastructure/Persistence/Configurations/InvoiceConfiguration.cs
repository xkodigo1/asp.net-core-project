using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable("Invoices");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Number)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(i => i.Date)
            .IsRequired();

        builder.Property(i => i.Subtotal)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(i => i.TaxRate)
            .HasPrecision(5, 2)
            .IsRequired();

        builder.Property(i => i.TaxAmount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(i => i.DiscountRate)
            .HasPrecision(5, 2)
            .IsRequired();

        builder.Property(i => i.DiscountAmount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(i => i.Total)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(i => i.PaymentMethod)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(i => i.PaymentStatus)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(i => i.Notes)
            .HasMaxLength(500);

        // Relaciones
        builder.HasOne(i => i.ServiceOrder)
            .WithOne(so => so.Invoice)
            .HasForeignKey<Invoice>(i => i.ServiceOrderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(i => !i.IsDeleted);
    }
} 