using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("Vehicles");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.Brand)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(v => v.Model)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(v => v.Year)
            .IsRequired();

        builder.Property(v => v.LicensePlate)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(v => v.VIN)
            .HasMaxLength(17)
            .IsRequired();

        builder.Property(v => v.Color)
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(v => v.EngineNumber)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(v => v.Mileage)
            .IsRequired();

        builder.Property(v => v.Notes)
            .HasMaxLength(500);

        // Relaciones
        builder.HasOne(v => v.Customer)
            .WithMany(c => c.Vehicles)
            .HasForeignKey(v => v.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(v => v.ServiceOrders)
            .WithOne(so => so.Vehicle)
            .HasForeignKey(so => so.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(v => !v.IsDeleted);
    }
} 