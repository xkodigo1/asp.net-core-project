using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class InventoryDetailConfiguration : IEntityTypeConfiguration<InventoryDetail>
{
    public void Configure(EntityTypeBuilder<InventoryDetail> builder)
    {
        builder.ToTable("InventoryDetails");

        builder.HasKey(id => id.Id);

        builder.Property(id => id.Quantity)
            .IsRequired();

        builder.Property(id => id.UnitCost)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(id => id.Total)
            .HasPrecision(18, 2)
            .IsRequired();

        // Relaciones
        builder.HasOne(id => id.Inventory)
            .WithMany(i => i.InventoryDetails)
            .HasForeignKey(id => id.InventoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(id => id.Spare)
            .WithMany(s => s.InventoryDetails)
            .HasForeignKey(id => id.SpareId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(id => !id.IsDeleted);
    }
} 