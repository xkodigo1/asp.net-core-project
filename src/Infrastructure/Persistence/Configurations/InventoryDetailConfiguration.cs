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
        builder.HasOne<Inventory>()
            .WithMany(i => i.InventoryDetails)
            .HasForeignKey("InventoryId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Spare>()
            .WithMany()
            .HasForeignKey("SpareId")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<ServiceOrder>()
            .WithMany(so => so.InventoryDetails)
            .HasForeignKey("ServiceOrderId")
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasQueryFilter(id => !id.IsDeleted);
    }
} 