using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IInventoryRepository : IRepository<Inventory>
{
    Task<IEnumerable<Spare>> GetLowStockSparesAsync(CancellationToken cancellationToken = default);
    Task<Inventory?> GetByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Inventory>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    Task<bool> HasSufficientStockAsync(Guid spareId, int requiredQuantity, CancellationToken cancellationToken = default);
    Task UpdateStockAsync(Guid spareId, int quantity, string documentType, CancellationToken cancellationToken = default);
} 