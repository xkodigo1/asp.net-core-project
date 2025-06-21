using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
{
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        builder.ToTable("Inventories");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.DocumentNumber)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(i => i.DocumentType)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(i => i.Date)
            .IsRequired();

        builder.Property(i => i.Notes)
            .HasMaxLength(500);

        // Relaciones
        builder.HasMany<InventoryDetail>()
            .WithOne()
            .HasForeignKey("InventoryId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(i => !i.IsDeleted);
    }
} 