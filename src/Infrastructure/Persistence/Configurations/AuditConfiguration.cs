using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class AuditConfiguration : IEntityTypeConfiguration<Audit>
{
    public void Configure(EntityTypeBuilder<Audit> builder)
    {
        builder.ToTable("Audits");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.AffectedEntity)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(a => a.ActionType)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(a => a.Timestamp)
            .IsRequired();

        builder.HasOne(a => a.ResponsibleUser)
            .WithMany(u => u.Audits)
            .HasForeignKey(a => a.ResponsibleUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Agregar el mismo filtro global que User
        builder.HasQueryFilter(a => !a.ResponsibleUser.IsDeleted);
    }
} 