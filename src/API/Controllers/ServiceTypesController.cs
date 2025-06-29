using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ServiceTypesController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ServiceTypesController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ServiceType>>> GetServiceTypes(CancellationToken cancellationToken)
    {
        var serviceTypes = await _unitOfWork.ServiceTypes.GetAllAsync(cancellationToken);
        return Ok(serviceTypes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceType>> GetServiceType(int id, CancellationToken cancellationToken)
    {
        var serviceType = await _unitOfWork.ServiceTypes.GetByIdAsync(id, cancellationToken);
        if (serviceType == null) return NotFound();

        return Ok(serviceType);
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<ServiceType>>> SearchServiceTypes(
        [FromQuery] string? name,
        [FromQuery] string? description,
        CancellationToken cancellationToken)
    {
        var serviceTypes = await _unitOfWork.Repository<ServiceType>()
            .FindAsync(st =>
                (!string.IsNullOrEmpty(name) ? st.Name.Contains(name) : true) &&
                (!string.IsNullOrEmpty(description) ? (st.Description != null && st.Description.Contains(description)) : true) &&
                !st.IsDeleted,
                cancellationToken);

        return Ok(serviceTypes);
    }

    [HttpPost]
    public async Task<ActionResult<ServiceType>> CreateServiceType([FromBody] ServiceType serviceType, CancellationToken cancellationToken)
    {
        // Validar si ya existe un tipo de servicio con el mismo nombre
        var existingServiceType = await _unitOfWork.Repository<ServiceType>()
            .FindAsync(st => st.Name == serviceType.Name && !st.IsDeleted, cancellationToken);

        if (existingServiceType.Any())
            return BadRequest("Ya existe un tipo de servicio con este nombre.");

        await _unitOfWork.ServiceTypes.AddAsync(serviceType, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return CreatedAtAction(nameof(GetServiceType), new { id = serviceType.Id }, serviceType);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ServiceType>> UpdateServiceType(int id, [FromBody] ServiceType serviceType, CancellationToken cancellationToken)
    {
        if (id != serviceType.Id)
            return BadRequest();

        var existingServiceType = await _unitOfWork.ServiceTypes.GetByIdAsync(id, cancellationToken);
        if (existingServiceType == null)
            return NotFound();

        // Validar si el nuevo nombre ya está en uso por otro tipo de servicio
        if (existingServiceType.Name != serviceType.Name)
        {
            var duplicateServiceType = await _unitOfWork.Repository<ServiceType>()
                .FindAsync(st => st.Name == serviceType.Name && st.Id != id && !st.IsDeleted, cancellationToken);

            if (duplicateServiceType.Any())
                return BadRequest("Ya existe otro tipo de servicio con este nombre.");
        }

        await _unitOfWork.ServiceTypes.UpdateAsync(serviceType, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Ok(serviceType);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteServiceType(int id, CancellationToken cancellationToken)
    {
        var serviceType = await _unitOfWork.ServiceTypes.GetByIdAsync(id, cancellationToken);
        if (serviceType == null) return NotFound();

        // Verificar si el tipo de servicio está siendo usado en órdenes de servicio
        var hasServiceOrders = await _unitOfWork.Repository<ServiceOrder>()
            .AnyAsync(so => so.ServiceTypeId == id && !so.IsDeleted, cancellationToken);

        if (hasServiceOrders)
            return BadRequest("No se puede eliminar el tipo de servicio porque está siendo usado en órdenes de servicio.");

        await _unitOfWork.ServiceTypes.SoftDeleteAsync(serviceType, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
} 