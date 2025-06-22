using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IServiceOrderRepository : IRepository<ServiceOrder>
{
    Task<ServiceOrder?> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<ServiceOrder>> GetByStatusAsync(int statusId, CancellationToken cancellationToken = default);
    Task<IEnumerable<ServiceOrder>> GetByMechanicAsync(int mechanicId, CancellationToken cancellationToken = default);
    Task<IEnumerable<ServiceOrder>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    Task<IEnumerable<ServiceOrder>> GetByCustomerAsync(int customerId, CancellationToken cancellationToken = default);
    Task<IEnumerable<ServiceOrder>> GetByVehicleAsync(int vehicleId, CancellationToken cancellationToken = default);
} 