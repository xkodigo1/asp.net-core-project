using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IUnitOfWork
{
    IRepository<User> Users { get; }
    IRepository<Role> Roles { get; }
    IRepository<ServiceOrder> ServiceOrders { get; }
    IRepository<Customer> Customers { get; }
    IRepository<Vehicle> Vehicles { get; }
    IRepository<Spare> Spares { get; }
    IRepository<Inventory> Inventories { get; }
    IRepository<Invoice> Invoices { get; }
    IRepository<ServiceType> ServiceTypes { get; }
    IRepository<Status> Statuses { get; }
    IRepository<Specialization> Specializations { get; }

    // Extended repositories with custom operations
    IUserRepository UsersExtended { get; }
    IServiceOrderRepository ServiceOrdersExtended { get; }
    IInventoryRepository InventoriesExtended { get; }

    // Generic repository access
    IRepository<T> Repository<T>() where T : BaseEntity;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
} 