using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class DiagnosticDetailConfiguration : IEntityTypeConfiguration<DiagnosticDetail>
{
    public void Configure(EntityTypeBuilder<DiagnosticDetail> builder)
    {
        builder.ToTable("DiagnosticDetails");

        builder.HasKey(dd => dd.Id);

        builder.Property(dd => dd.Description)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(dd => dd.Observation)
            .HasMaxLength(500);

        builder.Property(dd => dd.EstimatedCost)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(dd => dd.Priority)
            .IsRequired();

        // Relaciones
        builder.HasOne(dd => dd.Diagnostic)
            .WithMany(d => d.DiagnosticDetails)
            .HasForeignKey(dd => dd.DiagnosticId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(dd => dd.ServiceOrder)
            .WithMany(so => so.DiagnosticDetails)
            .HasForeignKey(dd => dd.ServiceOrderId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasQueryFilter(dd => !dd.IsDeleted);
    }
} 