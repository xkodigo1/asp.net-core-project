using Application.Common.DTOs.Inventory;
using Application.Common.DTOs.Spares;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class InventoryController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public InventoryController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InventoryDto>>> GetInventoryMovements(CancellationToken cancellationToken)
    {
        var movements = await _unitOfWork.Inventories.GetAllAsync(cancellationToken);
        return Ok(_mapper.Map<IEnumerable<InventoryDto>>(movements));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InventoryDto>> GetInventoryMovement(int id, CancellationToken cancellationToken)
    {
        var movement = await _unitOfWork.InventoriesExtended.GetByIdWithDetailsAsync(id, cancellationToken);
        if (movement == null) return NotFound();

        return Ok(_mapper.Map<InventoryDto>(movement));
    }

    [HttpGet("low-stock")]
    public async Task<ActionResult<IEnumerable<SpareDto>>> GetLowStockSpares(CancellationToken cancellationToken)
    {
        var spares = await _unitOfWork.InventoriesExtended.GetLowStockSparesAsync(cancellationToken);
        return Ok(_mapper.Map<IEnumerable<SpareDto>>(spares));
    }

    [HttpGet("by-date-range")]
    public async Task<ActionResult<IEnumerable<InventoryDto>>> GetByDateRange(
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate,
        CancellationToken cancellationToken)
    {
        var movements = await _unitOfWork.InventoriesExtended
            .GetByDateRangeAsync(startDate, endDate, cancellationToken);

        return Ok(_mapper.Map<IEnumerable<InventoryDto>>(movements));
    }

    [HttpPost("check-stock")]
    public async Task<ActionResult<bool>> CheckStock(
        [FromQuery] int spareId,
        [FromQuery] int quantity,
        CancellationToken cancellationToken)
    {
        var hasStock = await _unitOfWork.InventoriesExtended
            .HasSufficientStockAsync(spareId, quantity, cancellationToken);

        return Ok(hasStock);
    }

    [HttpPost("entry")]
    public async Task<ActionResult<InventoryDto>> RegisterEntry(CreateInventoryDto createDto, CancellationToken cancellationToken)
    {
        // Validar que el repuesto existe
        var spare = await _unitOfWork.Spares.GetByIdAsync(createDto.SpareId, cancellationToken);
        if (spare == null)
            return BadRequest("El repuesto especificado no existe.");

        var inventory = new Inventory
        {
            DocumentType = "ENTRADA",
            DocumentNumber = DateTime.Now.ToString("yyyyMMddHHmmss"),
            Date = DateTime.Now,
            Notes = createDto.Notes ?? string.Empty
        };

        await _unitOfWork.Inventories.AddAsync(inventory, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var detail = new InventoryDetail
        {
            InventoryId = inventory.Id,
            SpareId = createDto.SpareId,
            Quantity = createDto.Quantity,
            UnitCost = createDto.UnitCost,
            BatchNumber = createDto.BatchNumber ?? string.Empty,
            ExpirationDate = createDto.ExpirationDate,
            Location = createDto.Location
        };

        await _unitOfWork.Repository<InventoryDetail>().AddAsync(detail, cancellationToken);

        // Actualizar el stock del repuesto
        await _unitOfWork.InventoriesExtended.UpdateStockAsync(
            createDto.SpareId,
            createDto.Quantity,
            "ENTRADA",
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var result = await _unitOfWork.InventoriesExtended.GetByIdWithDetailsAsync(inventory.Id, cancellationToken);
        return Ok(_mapper.Map<InventoryDto>(result));
    }

    [HttpPost("exit")]
    public async Task<ActionResult<InventoryDto>> RegisterExit(CreateInventoryDto createDto, CancellationToken cancellationToken)
    {
        // Validar que el repuesto existe
        var spare = await _unitOfWork.Spares.GetByIdAsync(createDto.SpareId, cancellationToken);
        if (spare == null)
            return BadRequest("El repuesto especificado no existe.");

        // Validar que hay suficiente stock
        var hasStock = await _unitOfWork.InventoriesExtended
            .HasSufficientStockAsync(createDto.SpareId, createDto.Quantity, cancellationToken);

        if (!hasStock)
            return BadRequest("No hay suficiente stock para realizar la salida.");

        var inventory = new Inventory
        {
            DocumentType = "SALIDA",
            DocumentNumber = DateTime.Now.ToString("yyyyMMddHHmmss"),
            Date = DateTime.Now,
            Notes = createDto.Notes ?? string.Empty
        };

        await _unitOfWork.Inventories.AddAsync(inventory, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var detail = new InventoryDetail
        {
            InventoryId = inventory.Id,
            SpareId = createDto.SpareId,
            Quantity = -createDto.Quantity, // Cantidad negativa para salidas
            UnitCost = createDto.UnitCost,
            BatchNumber = createDto.BatchNumber ?? string.Empty,
            ExpirationDate = createDto.ExpirationDate,
            Location = createDto.Location
        };

        await _unitOfWork.Repository<InventoryDetail>().AddAsync(detail, cancellationToken);

        // Actualizar el stock del repuesto
        await _unitOfWork.InventoriesExtended.UpdateStockAsync(
            createDto.SpareId,
            -createDto.Quantity, // Cantidad negativa para salidas
            "SALIDA",
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var result = await _unitOfWork.InventoriesExtended.GetByIdWithDetailsAsync(inventory.Id, cancellationToken);
        return Ok(_mapper.Map<InventoryDto>(result));
    }
} 