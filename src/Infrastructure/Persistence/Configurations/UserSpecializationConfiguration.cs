using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class UserSpecializationConfiguration : IEntityTypeConfiguration<UserSpecialization>
{
    public void Configure(EntityTypeBuilder<UserSpecialization> builder)
    {
        builder.ToTable("UserSpecializations");

        builder.HasKey(us => new { us.UserId, us.SpecializationId });

        // Relaciones
        builder.HasOne(us => us.User)
            .WithMany()
            .HasForeignKey(us => us.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(us => us.Specialization)
            .WithMany()
            .HasForeignKey(us => us.SpecializationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(us => !us.IsDeleted);
    }
} 