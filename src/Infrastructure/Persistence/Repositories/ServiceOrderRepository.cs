using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ServiceOrderRepository : BaseRepository<ServiceOrder>, IServiceOrderRepository
{
    public ServiceOrderRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<ServiceOrder?> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(so => so.Vehicle)
                .ThenInclude(v => v.Customer)
            .Include(so => so.Mechanic)
            .Include(so => so.ServiceType)
            .Include(so => so.Status)
            .Include(so => so.DiagnosticDetails)
                .ThenInclude(dd => dd.Diagnostic)
            .Include(so => so.OrderDetails)
                .ThenInclude(od => od.Spare)
            .Include(so => so.Invoice)
            .FirstOrDefaultAsync(so => so.Id == id && !so.IsDeleted, cancellationToken);
    }

    public async Task<IEnumerable<ServiceOrder>> GetByStatusAsync(int statusId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(so => so.Vehicle)
                .ThenInclude(v => v.Customer)
            .Include(so => so.Status)
            .Where(so => so.StatusId == statusId && !so.IsDeleted)
            .OrderByDescending(so => so.EntryDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ServiceOrder>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(so => so.Vehicle)
                .ThenInclude(v => v.Customer)
            .Include(so => so.Status)
            .Where(so => so.EntryDate >= startDate && so.EntryDate <= endDate && !so.IsDeleted)
            .OrderByDescending(so => so.EntryDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ServiceOrder>> GetByMechanicAsync(int mechanicId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(so => so.Vehicle)
                .ThenInclude(v => v.Customer)
            .Include(so => so.Status)
            .Where(so => so.MechanicId == mechanicId && !so.IsDeleted)
            .OrderByDescending(so => so.EntryDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ServiceOrder>> GetByCustomerAsync(int customerId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(so => so.Vehicle)
                .ThenInclude(v => v.Customer)
            .Include(so => so.Status)
            .Where(so => so.Vehicle.CustomerId == customerId && !so.IsDeleted)
            .OrderByDescending(so => so.EntryDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ServiceOrder>> GetByVehicleAsync(int vehicleId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(so => so.Vehicle)
                .ThenInclude(v => v.Customer)
            .Include(so => so.Status)
            .Where(so => so.VehicleId == vehicleId && !so.IsDeleted)
            .OrderByDescending(so => so.EntryDate)
            .ToListAsync(cancellationToken);
    }
} 