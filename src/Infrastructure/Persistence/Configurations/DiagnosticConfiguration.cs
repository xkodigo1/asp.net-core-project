using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class DiagnosticConfiguration : IEntityTypeConfiguration<Diagnostic>
{
    public void Configure(EntityTypeBuilder<Diagnostic> builder)
    {
        builder.ToTable("Diagnostics");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Description)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(d => d.Recommendation)
            .HasMaxLength(500);

        builder.Property(d => d.EstimatedCost)
            .HasPrecision(18, 2)
            .IsRequired();

        // Relaciones
        builder.HasMany<DiagnosticDetail>()
            .WithOne()
            .HasForeignKey("DiagnosticId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(d => !d.IsDeleted);
    }
} 