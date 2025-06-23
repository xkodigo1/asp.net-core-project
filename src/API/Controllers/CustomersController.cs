using Application.Common.DTOs.Customers;
using Application.Common.DTOs.Vehicles;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CustomersController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CustomersController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers(CancellationToken cancellationToken)
    {
        var customers = await _unitOfWork.Customers.GetAllAsync(cancellationToken);
        return Ok(_mapper.Map<IEnumerable<CustomerDto>>(customers));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerDto>> GetCustomer(int id, CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.Customers.GetByIdAsync(id, cancellationToken);
        if (customer == null) return NotFound();

        return Ok(_mapper.Map<CustomerDto>(customer));
    }

    [HttpGet("{id}/vehicles")]
    public async Task<ActionResult<IEnumerable<VehicleDto>>> GetCustomerVehicles(int id, CancellationToken cancellationToken)
    {
        var vehicles = await _unitOfWork.Repository<Vehicle>()
            .FindAsync(v => v.CustomerId == id && !v.IsDeleted, cancellationToken);

        return Ok(_mapper.Map<IEnumerable<VehicleDto>>(vehicles));
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<CustomerDto>>> SearchCustomers(
        [FromQuery] string? name,
        [FromQuery] string? email,
        [FromQuery] string? phone,
        CancellationToken cancellationToken)
    {
        var customers = await _unitOfWork.Repository<Customer>()
            .FindAsync(c => 
                (!string.IsNullOrEmpty(name) ? (c.FirstName + " " + c.LastName).Contains(name) : true) &&
                (!string.IsNullOrEmpty(email) ? c.Email.Contains(email) : true) &&
                (!string.IsNullOrEmpty(phone) ? c.Phone.Contains(phone) : true) &&
                !c.IsDeleted, 
                cancellationToken);

        return Ok(_mapper.Map<IEnumerable<CustomerDto>>(customers));
    }

    [HttpPost]
    public async Task<ActionResult<CustomerDto>> CreateCustomer(CreateCustomerDto createDto, CancellationToken cancellationToken)
    {
        // Validar si ya existe un cliente con el mismo email
        var existingCustomer = await _unitOfWork.Repository<Customer>()
            .FindAsync(c => c.Email == createDto.Email && !c.IsDeleted, cancellationToken);

        if (existingCustomer.Any())
            return BadRequest("Ya existe un cliente con este email.");

        var customer = new Customer
        {
            FirstName = createDto.FirstName,
            LastName = createDto.LastName,
            Email = createDto.Email,
            Phone = createDto.Phone,
            Address = createDto.Address
        };

        await _unitOfWork.Customers.AddAsync(customer, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, _mapper.Map<CustomerDto>(customer));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CustomerDto>> UpdateCustomer(int id, UpdateCustomerDto updateDto, CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.Customers.GetByIdAsync(id, cancellationToken);
        if (customer == null) return NotFound();

        // Validar si el nuevo email ya está en uso por otro cliente
        if (customer.Email != updateDto.Email)
        {
            var existingCustomer = await _unitOfWork.Repository<Customer>()
                .FindAsync(c => c.Email == updateDto.Email && c.Id != id && !c.IsDeleted, cancellationToken);

            if (existingCustomer.Any())
                return BadRequest("Ya existe otro cliente con este email.");
        }

        customer.FirstName = updateDto.FirstName;
        customer.LastName = updateDto.LastName;
        customer.Email = updateDto.Email;
        customer.Phone = updateDto.Phone;
        customer.Address = updateDto.Address;

        await _unitOfWork.Customers.UpdateAsync(customer, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Ok(_mapper.Map<CustomerDto>(customer));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id, CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.Customers.GetByIdAsync(id, cancellationToken);
        if (customer == null) return NotFound();

        // Verificar si el cliente tiene vehículos asociados
        var hasVehicles = await _unitOfWork.Repository<Vehicle>()
            .AnyAsync(v => v.CustomerId == id && !v.IsDeleted, cancellationToken);

        if (hasVehicles)
            return BadRequest("No se puede eliminar el cliente porque tiene vehículos asociados.");

        await _unitOfWork.Customers.SoftDeleteAsync(customer, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
} 