using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Role> Roles { get; }
    DbSet<Specialization> Specializations { get; }
    DbSet<ServiceType> ServiceTypes { get; }
    DbSet<ServiceOrder> ServiceOrders { get; }
    DbSet<Spare> Spares { get; }
    DbSet<Inventory> Inventories { get; }
    DbSet<Invoice> Invoices { get; }
    DbSet<Status> Statuses { get; }
    DbSet<Vehicle> Vehicles { get; }
    DbSet<Customer> Customers { get; }
    DbSet<Diagnostic> Diagnostics { get; }
    DbSet<DiagnosticDetail> DiagnosticDetails { get; }
    DbSet<OrderDetail> OrderDetails { get; }
    DbSet<InventoryDetail> InventoryDetails { get; }
    DbSet<UserRole> UserRoles { get; }
    DbSet<UserSpecialization> UserSpecializations { get; }
    DbSet<Audit> Audits { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
} 