using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IDbContextTransaction? _transaction;
    private bool _disposed;

    // Specific repositories
    private UserRepository? _users;
    private ServiceOrderRepository? _serviceOrders;
    private InventoryRepository? _inventory;

    // Generic repositories
    private IRepository<Role>? _roles;
    private IRepository<Specialization>? _specializations;
    private IRepository<ServiceType>? _serviceTypes;
    private IRepository<Spare>? _spares;
    private IRepository<Invoice>? _invoices;
    private IRepository<Status>? _statuses;
    private IRepository<Vehicle>? _vehicles;
    private IRepository<Customer>? _customers;
    private IRepository<Diagnostic>? _diagnostics;
    private IRepository<DiagnosticDetail>? _diagnosticDetails;
    private IRepository<OrderDetail>? _orderDetails;
    private IRepository<InventoryDetail>? _inventoryDetails;
    private IRepository<UserRole>? _userRoles;
    private IRepository<UserSpecialization>? _userSpecializations;
    private IRepository<Audit>? _audits;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    // Specific repository properties
    public IRepository<User> Users => _users ??= new UserRepository(_context);
    public IRepository<ServiceOrder> ServiceOrders => _serviceOrders ??= new ServiceOrderRepository(_context);
    public IRepository<Inventory> Inventories => _inventory ??= new InventoryRepository(_context);

    // Generic repository properties
    public IRepository<Role> Roles => _roles ??= new BaseRepository<Role>(_context);
    public IRepository<Specialization> Specializations => _specializations ??= new BaseRepository<Specialization>(_context);
    public IRepository<ServiceType> ServiceTypes => _serviceTypes ??= new BaseRepository<ServiceType>(_context);
    public IRepository<Spare> Spares => _spares ??= new BaseRepository<Spare>(_context);
    public IRepository<Invoice> Invoices => _invoices ??= new BaseRepository<Invoice>(_context);
    public IRepository<Status> Statuses => _statuses ??= new BaseRepository<Status>(_context);
    public IRepository<Vehicle> Vehicles => _vehicles ??= new BaseRepository<Vehicle>(_context);
    public IRepository<Customer> Customers => _customers ??= new BaseRepository<Customer>(_context);
    public IRepository<Diagnostic> Diagnostics => _diagnostics ??= new BaseRepository<Diagnostic>(_context);
    public IRepository<DiagnosticDetail> DiagnosticDetails => _diagnosticDetails ??= new BaseRepository<DiagnosticDetail>(_context);
    public IRepository<OrderDetail> OrderDetails => _orderDetails ??= new BaseRepository<OrderDetail>(_context);
    public IRepository<InventoryDetail> InventoryDetails => _inventoryDetails ??= new BaseRepository<InventoryDetail>(_context);
    public IRepository<UserRole> UserRoles => _userRoles ??= new BaseRepository<UserRole>(_context);
    public IRepository<UserSpecialization> UserSpecializations => _userSpecializations ??= new BaseRepository<UserSpecialization>(_context);
    public IRepository<Audit> Audits => _audits ??= new BaseRepository<Audit>(_context);

    // Helper methods to access specific repository functionality
    public UserRepository UsersExtended => (UserRepository)Users;
    public ServiceOrderRepository ServiceOrdersExtended => (ServiceOrderRepository)ServiceOrders;
    public InventoryRepository InventoriesExtended => (InventoryRepository)Inventories;

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await SaveChangesAsync(cancellationToken);
            if (_transaction != null)
            {
                await _transaction.CommitAsync(cancellationToken);
            }
        }
        catch
        {
            await RollbackTransactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _context.Dispose();
            _transaction?.Dispose();
        }
        _disposed = true;
    }
} 