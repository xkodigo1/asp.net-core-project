using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class SpecializationsController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SpecializationsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Specialization>>> GetSpecializations(CancellationToken cancellationToken)
    {
        var specializations = await _unitOfWork.Specializations.GetAllAsync(cancellationToken);
        return Ok(specializations);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Specialization>> GetSpecialization(int id, CancellationToken cancellationToken)
    {
        var specialization = await _unitOfWork.Specializations.GetByIdAsync(id, cancellationToken);
        if (specialization == null) return NotFound();

        return Ok(specialization);
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<Specialization>>> SearchSpecializations(
        [FromQuery] string? name,
        [FromQuery] string? description,
        CancellationToken cancellationToken)
    {
        var specializations = await _unitOfWork.Repository<Specialization>()
            .FindAsync(s =>
                (!string.IsNullOrEmpty(name) ? s.Name.Contains(name) : true) &&
                (!string.IsNullOrEmpty(description) ? (s.Description != null && s.Description.Contains(description)) : true) &&
                !s.IsDeleted,
                cancellationToken);

        return Ok(specializations);
    }

    [HttpGet("{id}/technicians")]
    public async Task<ActionResult<IEnumerable<User>>> GetTechnicians(int id, CancellationToken cancellationToken)
    {
        var specialization = await _unitOfWork.Specializations.GetByIdAsync(id, cancellationToken);
        if (specialization == null) return NotFound();

        var technicians = await _unitOfWork.Repository<UserSpecialization>()
            .FindAsync(us => us.SpecializationId == id && !us.IsDeleted, cancellationToken);

        var technicianIds = technicians.Select(t => t.UserId);
        var users = await _unitOfWork.Repository<User>()
            .FindAsync(u => technicianIds.Contains(u.Id) && !u.IsDeleted, cancellationToken);

        return Ok(users);
    }

    [HttpPost]
    public async Task<ActionResult<Specialization>> CreateSpecialization([FromBody] Specialization specialization, CancellationToken cancellationToken)
    {
        // Validar si ya existe una especialización con el mismo nombre
        var existingSpecialization = await _unitOfWork.Repository<Specialization>()
            .FindAsync(s => s.Name == specialization.Name && !s.IsDeleted, cancellationToken);

        if (existingSpecialization.Any())
            return BadRequest("Ya existe una especialización con este nombre.");

        await _unitOfWork.Specializations.AddAsync(specialization, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return CreatedAtAction(nameof(GetSpecialization), new { id = specialization.Id }, specialization);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Specialization>> UpdateSpecialization(int id, [FromBody] Specialization specialization, CancellationToken cancellationToken)
    {
        if (id != specialization.Id)
            return BadRequest();

        var existingSpecialization = await _unitOfWork.Specializations.GetByIdAsync(id, cancellationToken);
        if (existingSpecialization == null)
            return NotFound();

        // Validar si el nuevo nombre ya está en uso por otra especialización
        if (existingSpecialization.Name != specialization.Name)
        {
            var duplicateSpecialization = await _unitOfWork.Repository<Specialization>()
                .FindAsync(s => s.Name == specialization.Name && s.Id != id && !s.IsDeleted, cancellationToken);

            if (duplicateSpecialization.Any())
                return BadRequest("Ya existe otra especialización con este nombre.");
        }

        await _unitOfWork.Specializations.UpdateAsync(specialization, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Ok(specialization);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSpecialization(int id, CancellationToken cancellationToken)
    {
        var specialization = await _unitOfWork.Specializations.GetByIdAsync(id, cancellationToken);
        if (specialization == null) return NotFound();

        // Verificar si la especialización está asignada a técnicos
        var hasTechnicians = await _unitOfWork.Repository<UserSpecialization>()
            .AnyAsync(us => us.SpecializationId == id && !us.IsDeleted, cancellationToken);

        if (hasTechnicians)
            return BadRequest("No se puede eliminar la especialización porque está asignada a técnicos.");

        await _unitOfWork.Specializations.SoftDeleteAsync(specialization, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return NoContent();
    }

    [HttpPost("{id}/technicians/{userId}")]
    public async Task<IActionResult> AssignTechnician(int id, int userId, CancellationToken cancellationToken)
    {
        var specialization = await _unitOfWork.Specializations.GetByIdAsync(id, cancellationToken);
        if (specialization == null) return NotFound("Especialización no encontrada.");

        var user = await _unitOfWork.Users.GetByIdAsync(userId, cancellationToken);
        if (user == null) return NotFound("Usuario no encontrado.");

        // Verificar si ya existe la asignación
        var existingAssignment = await _unitOfWork.Repository<UserSpecialization>()
            .FindAsync(us => us.SpecializationId == id && us.UserId == userId && !us.IsDeleted, cancellationToken);

        if (existingAssignment.Any())
            return BadRequest("El técnico ya tiene asignada esta especialización.");

        var userSpecialization = new UserSpecialization
        {
            UserId = userId,
            SpecializationId = id
        };

        await _unitOfWork.Repository<UserSpecialization>().AddAsync(userSpecialization, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id}/technicians/{userId}")]
    public async Task<IActionResult> UnassignTechnician(int id, int userId, CancellationToken cancellationToken)
    {
        var assignment = await _unitOfWork.Repository<UserSpecialization>()
            .FindAsync(us => us.SpecializationId == id && us.UserId == userId && !us.IsDeleted, cancellationToken);

        if (!assignment.Any())
            return NotFound("No se encontró la asignación especificada.");

        await _unitOfWork.Repository<UserSpecialization>().SoftDeleteAsync(assignment.First(), cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
} 