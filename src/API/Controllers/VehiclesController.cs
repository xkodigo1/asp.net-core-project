using Application.Common.DTOs.Vehicles;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class VehiclesController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public VehiclesController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VehicleDto>>> GetVehicles(CancellationToken cancellationToken)
    {
        var vehicles = await _unitOfWork.Vehicles.GetAllAsync(cancellationToken);
        return Ok(_mapper.Map<IEnumerable<VehicleDto>>(vehicles));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VehicleDto>> GetVehicle(int id, CancellationToken cancellationToken)
    {
        var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(id, cancellationToken);
        if (vehicle == null) return NotFound();

        return Ok(_mapper.Map<VehicleDto>(vehicle));
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<VehicleDto>>> SearchVehicles(
        [FromQuery] string? licensePlate,
        [FromQuery] string? vin,
        [FromQuery] string? brand,
        [FromQuery] string? model,
        CancellationToken cancellationToken)
    {
        var vehicles = await _unitOfWork.Repository<Vehicle>()
            .FindAsync(v =>
                (!string.IsNullOrEmpty(licensePlate) ? v.LicensePlate.Contains(licensePlate) : true) &&
                (!string.IsNullOrEmpty(vin) ? v.VIN.Contains(vin) : true) &&
                (!string.IsNullOrEmpty(brand) ? v.Brand.Contains(brand) : true) &&
                (!string.IsNullOrEmpty(model) ? v.Model.Contains(model) : true) &&
                !v.IsDeleted,
                cancellationToken);

        return Ok(_mapper.Map<IEnumerable<VehicleDto>>(vehicles));
    }

    [HttpPost]
    public async Task<ActionResult<VehicleDto>> CreateVehicle(CreateVehicleDto createDto, CancellationToken cancellationToken)
    {
        // Validar si existe el cliente
        var customer = await _unitOfWork.Customers.GetByIdAsync(createDto.CustomerId, cancellationToken);
        if (customer == null)
            return BadRequest("El cliente especificado no existe.");

        // Validar si ya existe un vehículo con la misma placa o VIN
        var existingVehicle = await _unitOfWork.Repository<Vehicle>()
            .FindAsync(v => 
                (v.LicensePlate == createDto.LicensePlate || v.VIN == createDto.VIN) && 
                !v.IsDeleted, 
                cancellationToken);

        if (existingVehicle.Any())
            return BadRequest("Ya existe un vehículo con esta placa o VIN.");

        var vehicle = new Vehicle
        {
            CustomerId = createDto.CustomerId,
            Brand = createDto.Brand,
            Model = createDto.Model,
            Year = createDto.Year,
            LicensePlate = createDto.LicensePlate,
            VIN = createDto.VIN,
            Color = createDto.Color,
            EngineNumber = createDto.EngineNumber,
            Notes = createDto.Notes
        };

        await _unitOfWork.Vehicles.AddAsync(vehicle, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return CreatedAtAction(nameof(GetVehicle), new { id = vehicle.Id }, _mapper.Map<VehicleDto>(vehicle));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<VehicleDto>> UpdateVehicle(int id, UpdateVehicleDto updateDto, CancellationToken cancellationToken)
    {
        var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(id, cancellationToken);
        if (vehicle == null) return NotFound();

        // Validar si existe el cliente
        if (vehicle.CustomerId != updateDto.CustomerId)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(updateDto.CustomerId, cancellationToken);
            if (customer == null)
                return BadRequest("El cliente especificado no existe.");
        }

        // Validar si ya existe otro vehículo con la misma placa o VIN
        if (vehicle.LicensePlate != updateDto.LicensePlate || vehicle.VIN != updateDto.VIN)
        {
            var existingVehicle = await _unitOfWork.Repository<Vehicle>()
                .FindAsync(v => 
                    (v.LicensePlate == updateDto.LicensePlate || v.VIN == updateDto.VIN) && 
                    v.Id != id && 
                    !v.IsDeleted, 
                    cancellationToken);

            if (existingVehicle.Any())
                return BadRequest("Ya existe otro vehículo con esta placa o VIN.");
        }

        vehicle.CustomerId = updateDto.CustomerId;
        vehicle.Brand = updateDto.Brand;
        vehicle.Model = updateDto.Model;
        vehicle.Year = updateDto.Year;
        vehicle.LicensePlate = updateDto.LicensePlate;
        vehicle.VIN = updateDto.VIN;
        vehicle.Color = updateDto.Color;
        vehicle.EngineNumber = updateDto.EngineNumber;
        vehicle.Notes = updateDto.Notes;

        await _unitOfWork.Vehicles.UpdateAsync(vehicle, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Ok(_mapper.Map<VehicleDto>(vehicle));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVehicle(int id, CancellationToken cancellationToken)
    {
        var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(id, cancellationToken);
        if (vehicle == null) return NotFound();

        // Verificar si el vehículo tiene órdenes de servicio asociadas
        var hasServiceOrders = await _unitOfWork.Repository<ServiceOrder>()
            .AnyAsync(so => so.VehicleId == id && !so.IsDeleted, cancellationToken);

        if (hasServiceOrders)
            return BadRequest("No se puede eliminar el vehículo porque tiene órdenes de servicio asociadas.");

        await _unitOfWork.Vehicles.SoftDeleteAsync(vehicle, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
} 