using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class SpareConfiguration : IEntityTypeConfiguration<Spare>
{
    public void Configure(EntityTypeBuilder<Spare> builder)
    {
        builder.ToTable("Spares");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(s => s.Description)
            .HasMaxLength(500);

        builder.Property(s => s.Brand)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(s => s.Model)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(s => s.SerialNumber)
            .HasMaxLength(50);

        builder.Property(s => s.UnitPrice)
            .HasPrecision(18, 2)
            .IsRequired();

        // Relaciones
        builder.HasMany<OrderDetail>()
            .WithOne()
            .HasForeignKey("SpareId")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany<InventoryDetail>()
            .WithOne()
            .HasForeignKey("SpareId")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(s => !s.IsDeleted);
    }
} 