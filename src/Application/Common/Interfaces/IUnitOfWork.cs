namespace Application.Common.Interfaces;

public interface IUnitOfWork : IDisposable
{
    // Repositories
    IRepository<Domain.Entities.User> Users { get; }
    IRepository<Domain.Entities.Role> Roles { get; }
    IRepository<Domain.Entities.Specialization> Specializations { get; }
    IRepository<Domain.Entities.ServiceType> ServiceTypes { get; }
    IRepository<Domain.Entities.ServiceOrder> ServiceOrders { get; }
    IRepository<Domain.Entities.Spare> Spares { get; }
    IRepository<Domain.Entities.Inventory> Inventories { get; }
    IRepository<Domain.Entities.Invoice> Invoices { get; }
    IRepository<Domain.Entities.Status> Statuses { get; }
    IRepository<Domain.Entities.Vehicle> Vehicles { get; }
    IRepository<Domain.Entities.Customer> Customers { get; }
    IRepository<Domain.Entities.Diagnostic> Diagnostics { get; }
    IRepository<Domain.Entities.DiagnosticDetail> DiagnosticDetails { get; }
    IRepository<Domain.Entities.OrderDetail> OrderDetails { get; }
    IRepository<Domain.Entities.InventoryDetail> InventoryDetails { get; }
    IRepository<Domain.Entities.UserRole> UserRoles { get; }
    IRepository<Domain.Entities.UserSpecialization> UserSpecializations { get; }
    IRepository<Domain.Entities.Audit> Audits { get; }

    // Transaction management
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
} 