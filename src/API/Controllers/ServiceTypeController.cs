using Application.Common.DTOs.ServiceTypes;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceTypeController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ServiceTypeController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    /// <summary>
    /// Obtiene todos los tipos de servicio
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ServiceTypeDto>>> GetAll(CancellationToken cancellationToken = default)
    {
        var serviceTypes = await _unitOfWork.ServiceTypes.GetAllAsync(cancellationToken);
        return Ok(_mapper.Map<IEnumerable<ServiceTypeDto>>(serviceTypes));
    }

    /// <summary>
    /// Obtiene un tipo de servicio por su ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceTypeDto>> GetById(int id, CancellationToken cancellationToken = default)
    {
        var serviceType = await _unitOfWork.ServiceTypes.GetByIdAsync(id, cancellationToken);
        if (serviceType == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<ServiceTypeDto>(serviceType));
    }

    /// <summary>
    /// Crea un nuevo tipo de servicio
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ServiceTypeDto>> Create(CreateServiceTypeDto createServiceTypeDto, CancellationToken cancellationToken = default)
    {
        var serviceType = _mapper.Map<ServiceType>(createServiceTypeDto);
        await _unitOfWork.ServiceTypes.AddAsync(serviceType, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        var serviceTypeDto = _mapper.Map<ServiceTypeDto>(serviceType);
        return CreatedAtAction(nameof(GetById), new { id = serviceType.Id }, serviceTypeDto);
    }

    /// <summary>
    /// Actualiza un tipo de servicio existente
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateServiceTypeDto updateServiceTypeDto, CancellationToken cancellationToken = default)
    {
        if (id != updateServiceTypeDto.Id)
        {
            return BadRequest();
        }

        var existingServiceType = await _unitOfWork.ServiceTypes.GetByIdAsync(id, cancellationToken);
        if (existingServiceType == null)
        {
            return NotFound();
        }

        _mapper.Map(updateServiceTypeDto, existingServiceType);
        await _unitOfWork.ServiceTypes.UpdateAsync(existingServiceType, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Elimina un tipo de servicio
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
    {
        var serviceType = await _unitOfWork.ServiceTypes.GetByIdAsync(id, cancellationToken);
        if (serviceType == null)
        {
            return NotFound();
        }

        await _unitOfWork.ServiceTypes.SoftDeleteAsync(serviceType, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return NoContent();
    }
}