using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class StatusController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public StatusController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Status>>> GetStatuses(CancellationToken cancellationToken)
    {
        var statuses = await _unitOfWork.Statuses.GetAllAsync(cancellationToken);
        return Ok(statuses);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Status>> GetStatus(int id, CancellationToken cancellationToken)
    {
        var status = await _unitOfWork.Statuses.GetByIdAsync(id, cancellationToken);
        if (status == null) return NotFound();

        return Ok(status);
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<Status>>> SearchStatuses(
        [FromQuery] string? name,
        [FromQuery] string? description,
        CancellationToken cancellationToken)
    {
        var statuses = await _unitOfWork.Repository<Status>()
            .FindAsync(s =>
                (!string.IsNullOrEmpty(name) ? s.Name.Contains(name) : true) &&
                (!string.IsNullOrEmpty(description) ? (s.Description != null && s.Description.Contains(description)) : true) &&
                !s.IsDeleted,
                cancellationToken);

        return Ok(statuses);
    }

    [HttpPost]
    public async Task<ActionResult<Status>> CreateStatus([FromBody] Status status, CancellationToken cancellationToken)
    {
        // Validar si ya existe un estado con el mismo nombre
        var existingStatus = await _unitOfWork.Repository<Status>()
            .FindAsync(s => s.Name == status.Name && !s.IsDeleted, cancellationToken);

        if (existingStatus.Any())
            return BadRequest("Ya existe un estado con este nombre.");

        await _unitOfWork.Statuses.AddAsync(status, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return CreatedAtAction(nameof(GetStatus), new { id = status.Id }, status);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Status>> UpdateStatus(int id, [FromBody] Status status, CancellationToken cancellationToken)
    {
        if (id != status.Id)
            return BadRequest();

        var existingStatus = await _unitOfWork.Statuses.GetByIdAsync(id, cancellationToken);
        if (existingStatus == null)
            return NotFound();

        // Validar si el nuevo nombre ya está en uso por otro estado
        if (existingStatus.Name != status.Name)
        {
            var duplicateStatus = await _unitOfWork.Repository<Status>()
                .FindAsync(s => s.Name == status.Name && s.Id != id && !s.IsDeleted, cancellationToken);

            if (duplicateStatus.Any())
                return BadRequest("Ya existe otro estado con este nombre.");
        }

        await _unitOfWork.Statuses.UpdateAsync(status, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Ok(status);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStatus(int id, CancellationToken cancellationToken)
    {
        var status = await _unitOfWork.Statuses.GetByIdAsync(id, cancellationToken);
        if (status == null) return NotFound();

        // Verificar si el estado está siendo usado en órdenes de servicio
        var hasServiceOrders = await _unitOfWork.Repository<ServiceOrder>()
            .AnyAsync(so => so.StatusId == id && !so.IsDeleted, cancellationToken);

        if (hasServiceOrders)
            return BadRequest("No se puede eliminar el estado porque está siendo usado en órdenes de servicio.");

        await _unitOfWork.Statuses.SoftDeleteAsync(status, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
} 