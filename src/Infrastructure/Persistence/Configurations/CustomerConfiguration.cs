using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.FirstName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.LastName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.Email)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.Phone)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(c => c.Address)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(c => c.TaxId)
            .HasMaxLength(20);

        builder.Property(c => c.CompanyName)
            .HasMaxLength(100);

        builder.Property(c => c.Identification)
            .HasMaxLength(20)
            .IsRequired();

        // Relaciones
        builder.HasMany(c => c.Vehicles)
            .WithOne(v => v.Customer)
            .HasForeignKey(v => v.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.ServiceOrders)
            .WithOne()
            .HasForeignKey("CustomerId")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(c => !c.IsDeleted);
    }
} 