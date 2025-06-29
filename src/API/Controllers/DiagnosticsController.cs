using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class DiagnosticsController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DiagnosticsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Diagnostic>>> GetDiagnostics(CancellationToken cancellationToken)
    {
        var diagnostics = await _unitOfWork.Repository<Diagnostic>().GetAllAsync(cancellationToken);
        return Ok(diagnostics);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Diagnostic>> GetDiagnostic(int id, CancellationToken cancellationToken)
    {
        var diagnostic = await _unitOfWork.Repository<Diagnostic>().GetByIdAsync(id, cancellationToken);
        if (diagnostic == null) return NotFound();

        return Ok(diagnostic);
    }

    [HttpGet("by-service-order/{serviceOrderId}")]
    public async Task<ActionResult<IEnumerable<Diagnostic>>> GetByServiceOrder(int serviceOrderId, CancellationToken cancellationToken)
    {
        var serviceOrder = await _unitOfWork.ServiceOrders.GetByIdAsync(serviceOrderId, cancellationToken);
        if (serviceOrder == null) return NotFound("Orden de servicio no encontrada.");

        var diagnostics = await _unitOfWork.Repository<Diagnostic>()
            .FindAsync(d => d.ServiceOrderId == serviceOrderId && !d.IsDeleted, cancellationToken);

        return Ok(diagnostics);
    }

    [HttpGet("by-vehicle/{vehicleId}")]
    public async Task<ActionResult<IEnumerable<Diagnostic>>> GetByVehicle(int vehicleId, CancellationToken cancellationToken)
    {
        var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(vehicleId, cancellationToken);
        if (vehicle == null) return NotFound("Vehículo no encontrado.");

        var serviceOrders = await _unitOfWork.Repository<ServiceOrder>()
            .FindAsync(so => so.VehicleId == vehicleId && !so.IsDeleted, cancellationToken);

        var serviceOrderIds = serviceOrders.Select(so => so.Id);
        var diagnostics = await _unitOfWork.Repository<Diagnostic>()
            .FindAsync(d => serviceOrderIds.Contains(d.ServiceOrderId) && !d.IsDeleted, cancellationToken);

        return Ok(diagnostics);
    }

    [HttpPost]
    public async Task<ActionResult<Diagnostic>> CreateDiagnostic([FromBody] Diagnostic diagnostic, CancellationToken cancellationToken)
    {
        // Validar que la orden de servicio existe
        var serviceOrder = await _unitOfWork.ServiceOrders.GetByIdAsync(diagnostic.ServiceOrderId, cancellationToken);
        if (serviceOrder == null)
            return BadRequest("La orden de servicio especificada no existe.");

        await _unitOfWork.Repository<Diagnostic>().AddAsync(diagnostic, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Crear los detalles del diagnóstico
        if (diagnostic.DiagnosticDetails != null && diagnostic.DiagnosticDetails.Any())
        {
            foreach (var detail in diagnostic.DiagnosticDetails)
            {
                detail.DiagnosticId = diagnostic.Id;
                await _unitOfWork.Repository<DiagnosticDetail>().AddAsync(detail, cancellationToken);
            }
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return CreatedAtAction(nameof(GetDiagnostic), new { id = diagnostic.Id }, diagnostic);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Diagnostic>> UpdateDiagnostic(int id, [FromBody] Diagnostic diagnostic, CancellationToken cancellationToken)
    {
        if (id != diagnostic.Id)
            return BadRequest();

        var existingDiagnostic = await _unitOfWork.Repository<Diagnostic>().GetByIdAsync(id, cancellationToken);
        if (existingDiagnostic == null)
            return NotFound();

        // Validar que la orden de servicio existe
        var serviceOrder = await _unitOfWork.ServiceOrders.GetByIdAsync(diagnostic.ServiceOrderId, cancellationToken);
        if (serviceOrder == null)
            return BadRequest("La orden de servicio especificada no existe.");

        await _unitOfWork.Repository<Diagnostic>().UpdateAsync(diagnostic, cancellationToken);

        // Actualizar los detalles del diagnóstico
        if (diagnostic.DiagnosticDetails != null && diagnostic.DiagnosticDetails.Any())
        {
            // Eliminar los detalles existentes
            var existingDetails = await _unitOfWork.Repository<DiagnosticDetail>()
                .FindAsync(dd => dd.DiagnosticId == id && !dd.IsDeleted, cancellationToken);

            foreach (var detail in existingDetails)
            {
                await _unitOfWork.Repository<DiagnosticDetail>().SoftDeleteAsync(detail, cancellationToken);
            }

            // Agregar los nuevos detalles
            foreach (var detail in diagnostic.DiagnosticDetails)
            {
                detail.DiagnosticId = diagnostic.Id;
                await _unitOfWork.Repository<DiagnosticDetail>().AddAsync(detail, cancellationToken);
            }
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Ok(diagnostic);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDiagnostic(int id, CancellationToken cancellationToken)
    {
        var diagnostic = await _unitOfWork.Repository<Diagnostic>().GetByIdAsync(id, cancellationToken);
        if (diagnostic == null) return NotFound();

        // Eliminar los detalles del diagnóstico
        var details = await _unitOfWork.Repository<DiagnosticDetail>()
            .FindAsync(dd => dd.DiagnosticId == id && !dd.IsDeleted, cancellationToken);

        foreach (var detail in details)
        {
            await _unitOfWork.Repository<DiagnosticDetail>().SoftDeleteAsync(detail, cancellationToken);
        }

        await _unitOfWork.Repository<Diagnostic>().SoftDeleteAsync(diagnostic, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return NoContent();
    }

    [HttpPost("{id}/details")]
    public async Task<ActionResult<DiagnosticDetail>> AddDetail(int id, [FromBody] DiagnosticDetail detail, CancellationToken cancellationToken)
    {
        var diagnostic = await _unitOfWork.Repository<Diagnostic>().GetByIdAsync(id, cancellationToken);
        if (diagnostic == null) return NotFound("Diagnóstico no encontrado.");

        detail.DiagnosticId = id;
        await _unitOfWork.Repository<DiagnosticDetail>().AddAsync(detail, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Ok(detail);
    }

    [HttpDelete("{id}/details/{detailId}")]
    public async Task<IActionResult> DeleteDetail(int id, int detailId, CancellationToken cancellationToken)
    {
        var diagnostic = await _unitOfWork.Repository<Diagnostic>().GetByIdAsync(id, cancellationToken);
        if (diagnostic == null) return NotFound("Diagnóstico no encontrado.");

        var detail = await _unitOfWork.Repository<DiagnosticDetail>()
            .FindAsync(dd => dd.Id == detailId && dd.DiagnosticId == id && !dd.IsDeleted, cancellationToken);

        if (!detail.Any())
            return NotFound("Detalle no encontrado.");

        await _unitOfWork.Repository<DiagnosticDetail>().SoftDeleteAsync(detail.First(), cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
} 