using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ServiceOrderConfiguration : IEntityTypeConfiguration<ServiceOrder>
{
    public void Configure(EntityTypeBuilder<ServiceOrder> builder)
    {
        builder.ToTable("ServiceOrders");

        builder.HasKey(so => so.Id);

        builder.Property(so => so.EntryDate)
            .IsRequired();

        builder.Property(so => so.CustomerMessage)
            .HasMaxLength(500);

        // Relaciones
        builder.HasOne(so => so.Vehicle)
            .WithMany(v => v.ServiceOrders)
            .HasForeignKey(so => so.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(so => so.Mechanic)
            .WithMany(m => m.ServiceOrders)
            .HasForeignKey(so => so.MechanicId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(so => so.ServiceType)
            .WithMany(st => st.ServiceOrders)
            .HasForeignKey(so => so.ServiceTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(so => so.Status)
            .WithMany(s => s.ServiceOrders)
            .HasForeignKey(so => so.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(so => so.DiagnosticDetails)
            .WithOne(dd => dd.ServiceOrder)
            .HasForeignKey(dd => dd.ServiceOrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(so => so.OrderDetails)
            .WithOne(od => od.ServiceOrder)
            .HasForeignKey(od => od.ServiceOrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(so => so.Invoice)
            .WithOne(i => i.ServiceOrder)
            .HasForeignKey<Invoice>(i => i.ServiceOrderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(so => !so.IsDeleted);
    }
} 