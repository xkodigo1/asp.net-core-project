using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ServiceTypeConfiguration : IEntityTypeConfiguration<ServiceType>
{
    public void Configure(EntityTypeBuilder<ServiceType> builder)
    {
        builder.ToTable("ServiceTypes");

        builder.HasKey(st => st.Id);

        builder.Property(st => st.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(st => st.Description)
            .HasMaxLength(500);

        builder.Property(st => st.EstimatedTime)
            .IsRequired();

        builder.Property(st => st.BasePrice)
            .HasPrecision(18, 2)
            .IsRequired();

        // Relaciones
        builder.HasMany(st => st.ServiceOrders)
            .WithOne(so => so.ServiceType)
            .HasForeignKey(so => so.ServiceTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(st => !st.IsDeleted);
    }
} 