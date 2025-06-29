using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// Controlador para gestionar las especializaciones de los técnicos del taller.
/// Proporciona endpoints para crear, consultar, actualizar y eliminar especializaciones,
/// así como para asignar y desasignar técnicos a las especializaciones.
/// </summary>
public class SpecializationsController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    /// <summary>
    /// Constructor del controlador de especializaciones
    /// </summary>
    /// <param name="unitOfWork">Instancia del patrón Unit of Work para acceso a datos</param>
    /// <param name="mapper">Instancia de AutoMapper para mapeo de objetos</param>
    public SpecializationsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    /// <summary>
    /// Obtiene todas las especializaciones activas
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Lista de especializaciones</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Specialization>>> GetSpecializations(CancellationToken cancellationToken)
    {
        var specializations = await _unitOfWork.Specializations.GetAllAsync(cancellationToken);
        return Ok(specializations);
    }

    /// <summary>
    /// Obtiene una especialización por su ID
    /// </summary>
    /// <param name="id">ID de la especialización</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Especialización encontrada o NotFound si no existe</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Specialization>> GetSpecialization(int id, CancellationToken cancellationToken)
    {
        var specialization = await _unitOfWork.Specializations.GetByIdAsync(id, cancellationToken);
        if (specialization == null) return NotFound();

        return Ok(specialization);
    }

    /// <summary>
    /// Busca especializaciones por nombre y/o descripción
    /// </summary>
    /// <param name="name">Nombre o parte del nombre a buscar</param>
    /// <param name="description">Descripción o parte de la descripción a buscar</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Lista de especializaciones que coinciden con los criterios de búsqueda</returns>
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

    /// <summary>
    /// Obtiene todos los técnicos asignados a una especialización
    /// </summary>
    /// <param name="id">ID de la especialización</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Lista de técnicos con la especialización especificada</returns>
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

    /// <summary>
    /// Crea una nueva especialización
    /// </summary>
    /// <param name="specialization">Datos de la especialización a crear</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Especialización creada</returns>
    /// <response code="201">Especialización creada exitosamente</response>
    /// <response code="400">Ya existe una especialización con el mismo nombre</response>
    [HttpPost]
    public async Task<ActionResult<Specialization>> CreateSpecialization([FromBody] Specialization specialization, CancellationToken cancellationToken)
    {
        var existingSpecialization = await _unitOfWork.Repository<Specialization>()
            .FindAsync(s => s.Name == specialization.Name && !s.IsDeleted, cancellationToken);

        if (existingSpecialization.Any())
            return BadRequest("Ya existe una especialización con este nombre.");

        await _unitOfWork.Specializations.AddAsync(specialization, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return CreatedAtAction(nameof(GetSpecialization), new { id = specialization.Id }, specialization);
    }

    /// <summary>
    /// Actualiza una especialización existente
    /// </summary>
    /// <param name="id">ID de la especialización a actualizar</param>
    /// <param name="specialization">Nuevos datos de la especialización</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Especialización actualizada</returns>
    /// <response code="200">Especialización actualizada exitosamente</response>
    /// <response code="400">ID no coincide o nombre duplicado</response>
    /// <response code="404">Especialización no encontrada</response>
    [HttpPut("{id}")]
    public async Task<ActionResult<Specialization>> UpdateSpecialization(int id, [FromBody] Specialization specialization, CancellationToken cancellationToken)
    {
        if (id != specialization.Id)
            return BadRequest();

        var existingSpecialization = await _unitOfWork.Specializations.GetByIdAsync(id, cancellationToken);
        if (existingSpecialization == null)
            return NotFound();

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

    /// <summary>
    /// Elimina una especialización (soft delete)
    /// </summary>
    /// <param name="id">ID de la especialización a eliminar</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>NoContent si se eliminó correctamente</returns>
    /// <response code="204">Especialización eliminada exitosamente</response>
    /// <response code="400">La especialización tiene técnicos asignados</response>
    /// <response code="404">Especialización no encontrada</response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSpecialization(int id, CancellationToken cancellationToken)
    {
        var specialization = await _unitOfWork.Specializations.GetByIdAsync(id, cancellationToken);
        if (specialization == null) return NotFound();

        var hasTechnicians = await _unitOfWork.Repository<UserSpecialization>()
            .AnyAsync(us => us.SpecializationId == id && !us.IsDeleted, cancellationToken);

        if (hasTechnicians)
            return BadRequest("No se puede eliminar la especialización porque está asignada a técnicos.");

        await _unitOfWork.Specializations.SoftDeleteAsync(specialization, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Asigna un técnico a una especialización
    /// </summary>
    /// <param name="id">ID de la especialización</param>
    /// <param name="userId">ID del técnico</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>NoContent si se asignó correctamente</returns>
    /// <response code="204">Técnico asignado exitosamente</response>
    /// <response code="400">El técnico ya tiene esta especialización</response>
    /// <response code="404">Especialización o técnico no encontrado</response>
    [HttpPost("{id}/technicians/{userId}")]
    public async Task<IActionResult> AssignTechnician(int id, int userId, CancellationToken cancellationToken)
    {
        var specialization = await _unitOfWork.Specializations.GetByIdAsync(id, cancellationToken);
        if (specialization == null) return NotFound("Especialización no encontrada.");

        var user = await _unitOfWork.Users.GetByIdAsync(userId, cancellationToken);
        if (user == null) return NotFound("Usuario no encontrado.");

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

    /// <summary>
    /// Desasigna un técnico de una especialización
    /// </summary>
    /// <param name="id">ID de la especialización</param>
    /// <param name="userId">ID del técnico</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>NoContent si se desasignó correctamente</returns>
    /// <response code="204">Técnico desasignado exitosamente</response>
    /// <response code="404">Asignación no encontrada</response>
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