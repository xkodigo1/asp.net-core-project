using Application.Common.DTOs.Spares;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class SparesController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SparesController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SpareDto>>> GetSpares(CancellationToken cancellationToken)
    {
        var spares = await _unitOfWork.Spares.GetAllAsync(cancellationToken);
        return Ok(_mapper.Map<IEnumerable<SpareDto>>(spares));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SpareDto>> GetSpare(int id, CancellationToken cancellationToken)
    {
        var spare = await _unitOfWork.Spares.GetByIdAsync(id, cancellationToken);
        if (spare == null) return NotFound();

        return Ok(_mapper.Map<SpareDto>(spare));
    }

    [HttpGet("low-stock")]
    public async Task<ActionResult<IEnumerable<SpareDto>>> GetLowStockSpares(CancellationToken cancellationToken)
    {
        var spares = await _unitOfWork.InventoriesExtended.GetLowStockSparesAsync(cancellationToken);
        return Ok(_mapper.Map<IEnumerable<SpareDto>>(spares));
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<SpareDto>>> SearchSpares(
        [FromQuery] string? name,
        [FromQuery] string? brand,
        [FromQuery] decimal? minPrice,
        [FromQuery] decimal? maxPrice,
        CancellationToken cancellationToken)
    {
        var spares = await _unitOfWork.Repository<Spare>()
            .FindAsync(s =>
                (!string.IsNullOrEmpty(name) ? s.Name.Contains(name) : true) &&
                (!string.IsNullOrEmpty(brand) ? s.Brand.Contains(brand) : true) &&
                (!minPrice.HasValue || s.UnitPrice >= minPrice) &&
                (!maxPrice.HasValue || s.UnitPrice <= maxPrice) &&
                !s.IsDeleted,
                cancellationToken);

        return Ok(_mapper.Map<IEnumerable<SpareDto>>(spares));
    }

    [HttpPost]
    public async Task<ActionResult<SpareDto>> CreateSpare(CreateSpareDto createDto, CancellationToken cancellationToken)
    {
        // Validar si ya existe un repuesto con el mismo nombre y marca
        var existingSpare = await _unitOfWork.Repository<Spare>()
            .FindAsync(s => s.Name == createDto.Name && s.Brand == createDto.Brand && !s.IsDeleted, cancellationToken);

        if (existingSpare.Any())
            return BadRequest("Ya existe un repuesto con este nombre y marca.");

        var spare = new Spare
        {
            Name = createDto.Name,
            Description = createDto.Description,
            Brand = createDto.Brand,
            Model = createDto.Model,
            SerialNumber = createDto.SerialNumber,
            UnitPrice = createDto.UnitPrice,
            StockQuantity = createDto.StockQuantity,
            MinimumStock = createDto.MinimumStock
        };

        await _unitOfWork.Spares.AddAsync(spare, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return CreatedAtAction(nameof(GetSpare), new { id = spare.Id }, _mapper.Map<SpareDto>(spare));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<SpareDto>> UpdateSpare(int id, UpdateSpareDto updateDto, CancellationToken cancellationToken)
    {
        var spare = await _unitOfWork.Spares.GetByIdAsync(id, cancellationToken);
        if (spare == null) return NotFound();

        // Validar si el nuevo nombre y marca ya están en uso por otro repuesto
        if (spare.Name != updateDto.Name || spare.Brand != updateDto.Brand)
        {
            var existingSpare = await _unitOfWork.Repository<Spare>()
                .FindAsync(s => s.Name == updateDto.Name && s.Brand == updateDto.Brand && s.Id != id && !s.IsDeleted, cancellationToken);

            if (existingSpare.Any())
                return BadRequest("Ya existe otro repuesto con este nombre y marca.");
        }

        spare.Name = updateDto.Name;
        spare.Description = updateDto.Description;
        spare.Brand = updateDto.Brand;
        spare.Model = updateDto.Model;
        spare.SerialNumber = updateDto.SerialNumber;
        spare.UnitPrice = updateDto.UnitPrice;
        spare.MinimumStock = updateDto.MinimumStock;

        await _unitOfWork.Spares.UpdateAsync(spare, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Ok(_mapper.Map<SpareDto>(spare));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSpare(int id, CancellationToken cancellationToken)
    {
        var spare = await _unitOfWork.Spares.GetByIdAsync(id, cancellationToken);
        if (spare == null) return NotFound();

        // Verificar si el repuesto tiene movimientos de inventario
        var hasInventoryMovements = await _unitOfWork.Repository<InventoryDetail>()
            .AnyAsync(i => i.SpareId == id && !i.IsDeleted, cancellationToken);

        if (hasInventoryMovements)
            return BadRequest("No se puede eliminar el repuesto porque tiene movimientos de inventario asociados.");

        // Verificar si el repuesto está en alguna orden de servicio
        var hasServiceOrders = await _unitOfWork.Repository<OrderDetail>()
            .AnyAsync(od => od.SpareId == id && !od.IsDeleted, cancellationToken);

        if (hasServiceOrders)
            return BadRequest("No se puede eliminar el repuesto porque está asociado a órdenes de servicio.");

        await _unitOfWork.Spares.SoftDeleteAsync(spare, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
} 