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

        builder.Property(i => i.InvoiceNumber)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(i => i.IssueDate)
            .IsRequired();

        builder.Property(i => i.DueDate)
            .IsRequired();

        builder.Property(i => i.Subtotal)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(i => i.Tax)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(i => i.Total)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(i => i.Notes)
            .HasMaxLength(500);

        // Relaciones
        builder.HasOne<ServiceOrder>()
            .WithOne(so => so.Invoice)
            .HasForeignKey<Invoice>("ServiceOrderId")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(i => !i.IsDeleted);
    }
} 