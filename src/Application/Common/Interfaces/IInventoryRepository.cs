using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IInventoryRepository : IRepository<Inventory>
{
    Task<IEnumerable<Spare>> GetLowStockSparesAsync(CancellationToken cancellationToken = default);
    Task<Inventory?> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Inventory>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    Task<bool> HasSufficientStockAsync(int spareId, int requiredQuantity, CancellationToken cancellationToken = default);
    Task UpdateStockAsync(int spareId, int quantity, string documentType, CancellationToken cancellationToken = default);
} 