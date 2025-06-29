using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class InventoryRepository : BaseRepository<Inventory>, IInventoryRepository
{
    public InventoryRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Spare>> GetLowStockSparesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<Spare>()
            .Where(s => s.StockQuantity <= s.MinimumStock && !s.IsDeleted)
            .OrderBy(s => s.StockQuantity)
            .ToListAsync(cancellationToken);
    }

    public async Task<Inventory?> GetByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(i => i.InventoryDetails)
                .ThenInclude(id => id.Spare)
            .FirstOrDefaultAsync(i => i.Id == id && !i.IsDeleted, cancellationToken);
    }

    public async Task<IEnumerable<Inventory>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(i => i.InventoryDetails)
                .ThenInclude(id => id.Spare)
            .Where(i => i.Date >= startDate && i.Date <= endDate && !i.IsDeleted)
            .OrderByDescending(i => i.Date)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> HasSufficientStockAsync(Guid spareId, int requiredQuantity, CancellationToken cancellationToken = default)
    {
        var spare = await _context.Set<Spare>()
            .FirstOrDefaultAsync(s => s.Id == spareId && !s.IsDeleted, cancellationToken);

        return spare != null && spare.StockQuantity >= requiredQuantity;
    }

    public async Task UpdateStockAsync(Guid spareId, int quantity, string documentType, CancellationToken cancellationToken = default)
    {
        var spare = await _context.Set<Spare>()
            .FirstOrDefaultAsync(s => s.Id == spareId && !s.IsDeleted, cancellationToken);

        if (spare != null)
        {
            var inventory = new Inventory
            {
                DocumentType = documentType,
                DocumentNumber = DateTime.Now.ToString("yyyyMMddHHmmss"),
                Date = DateTime.Now,
                Notes = $"Stock update for spare {spare.Name}"
            };

            await _dbSet.AddAsync(inventory, cancellationToken);

            // Necesitamos guardar los cambios para obtener el Id generado
            await _context.SaveChangesAsync(cancellationToken);

            var detail = new InventoryDetail
            {
                InventoryId = inventory.Id,
                SpareId = spareId,
                Quantity = quantity
            };

            await _context.Set<InventoryDetail>().AddAsync(detail, cancellationToken);

            spare.StockQuantity += quantity;
            _context.Set<Spare>().Update(spare);
        }
    }
} 