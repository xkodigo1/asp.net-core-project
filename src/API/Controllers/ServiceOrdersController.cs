using Application.Common.DTOs.ServiceOrders;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ServiceOrdersController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ServiceOrdersController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ServiceOrderDto>>> GetServiceOrders(CancellationToken cancellationToken)
    {
        var orders = await _unitOfWork.ServiceOrders.GetAllAsync(cancellationToken);
        return Ok(_mapper.Map<IEnumerable<ServiceOrderDto>>(orders));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceOrderDto>> GetServiceOrder(int id, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.ServiceOrdersExtended.GetByIdWithDetailsAsync(id, cancellationToken);
        if (order == null) return NotFound();
        return Ok(_mapper.Map<ServiceOrderDto>(order));
    }

    [HttpGet("by-status/{statusId}")]
    public async Task<ActionResult<IEnumerable<ServiceOrderDto>>> GetByStatus(int statusId, CancellationToken cancellationToken)
    {
        var orders = await _unitOfWork.ServiceOrdersExtended.GetByStatusAsync(statusId, cancellationToken);
        return Ok(_mapper.Map<IEnumerable<ServiceOrderDto>>(orders));
    }

    [HttpGet("by-mechanic/{mechanicId}")]
    public async Task<ActionResult<IEnumerable<ServiceOrderDto>>> GetByMechanic(int mechanicId, CancellationToken cancellationToken)
    {
        var orders = await _unitOfWork.ServiceOrdersExtended.GetByMechanicAsync(mechanicId, cancellationToken);
        return Ok(_mapper.Map<IEnumerable<ServiceOrderDto>>(orders));
    }

    [HttpGet("by-date-range")]
    public async Task<ActionResult<IEnumerable<ServiceOrderDto>>> GetByDateRange(
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate,
        CancellationToken cancellationToken)
    {
        var orders = await _unitOfWork.ServiceOrdersExtended.GetByDateRangeAsync(startDate, endDate, cancellationToken);
        return Ok(_mapper.Map<IEnumerable<ServiceOrderDto>>(orders));
    }

    [HttpGet("by-customer/{customerId}")]
    public async Task<ActionResult<IEnumerable<ServiceOrderDto>>> GetByCustomer(int customerId, CancellationToken cancellationToken)
    {
        var orders = await _unitOfWork.ServiceOrdersExtended.GetByCustomerAsync(customerId, cancellationToken);
        return Ok(_mapper.Map<IEnumerable<ServiceOrderDto>>(orders));
    }

    [HttpGet("by-vehicle/{vehicleId}")]
    public async Task<ActionResult<IEnumerable<ServiceOrderDto>>> GetByVehicle(int vehicleId, CancellationToken cancellationToken)
    {
        var orders = await _unitOfWork.ServiceOrdersExtended.GetByVehicleAsync(vehicleId, cancellationToken);
        return Ok(_mapper.Map<IEnumerable<ServiceOrderDto>>(orders));
    }

    [HttpPost]
    public async Task<ActionResult<ServiceOrderDto>> CreateServiceOrder(CreateServiceOrderDto createDto, CancellationToken cancellationToken)
    {
        var serviceOrder = new ServiceOrder
        {
            VehicleId = createDto.VehicleId,
            MechanicId = createDto.MechanicId,
            ServiceTypeId = createDto.ServiceTypeId,
            EntryDate = createDto.EntryDate,
            ExitDate = createDto.ExitDate,
            CustomerMessage = createDto.CustomerMessage
        };

        await _unitOfWork.ServiceOrders.AddAsync(serviceOrder, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Add diagnostic details
        foreach (var detail in createDto.DiagnosticDetails)
        {
            var diagnosticDetail = new DiagnosticDetail
            {
                ServiceOrderId = (int)serviceOrder.Id,
                Description = detail.Description,
                Observation = detail.Observation,
                EstimatedCost = detail.EstimatedCost,
                Priority = detail.Priority
            };
            await _unitOfWork.Repository<DiagnosticDetail>().AddAsync(diagnosticDetail, cancellationToken);
        }

        // Add order details
        foreach (var detail in createDto.OrderDetails)
        {
            var orderDetail = new OrderDetail
            {
                ServiceOrderId = (int)serviceOrder.Id,
                SpareId = detail.SpareId,
                Quantity = detail.Quantity,
                UnitPrice = detail.UnitPrice,
                Discount = detail.Discount,
                Total = detail.Quantity * detail.UnitPrice * (1 - (detail.Discount ?? 0) / 100)
            };
            await _unitOfWork.Repository<OrderDetail>().AddAsync(orderDetail, cancellationToken);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var createdOrder = await _unitOfWork.ServiceOrdersExtended.GetByIdWithDetailsAsync(serviceOrder.Id, cancellationToken);
        return Ok(_mapper.Map<ServiceOrderDto>(createdOrder));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ServiceOrderDto>> UpdateServiceOrder(int id, UpdateServiceOrderDto updateDto, CancellationToken cancellationToken)
    {
        var serviceOrder = await _unitOfWork.ServiceOrdersExtended.GetByIdWithDetailsAsync(id, cancellationToken);
        if (serviceOrder == null) return NotFound();

        serviceOrder.VehicleId = updateDto.VehicleId;
        serviceOrder.MechanicId = updateDto.MechanicId;
        serviceOrder.ServiceTypeId = updateDto.ServiceTypeId;
        serviceOrder.StatusId = updateDto.StatusId;
        serviceOrder.EntryDate = updateDto.EntryDate;
        serviceOrder.ExitDate = updateDto.ExitDate;
        serviceOrder.CustomerMessage = updateDto.CustomerMessage;

        // Update diagnostic details
        var existingDiagnostics = await _unitOfWork.Repository<DiagnosticDetail>()
            .FindAsync(d => d.ServiceOrderId == id, cancellationToken);
        
        foreach (var diagnostic in existingDiagnostics)
        {
            await _unitOfWork.Repository<DiagnosticDetail>().DeleteAsync(diagnostic, cancellationToken);
        }

        foreach (var detail in updateDto.DiagnosticDetails)
        {
            var diagnosticDetail = new DiagnosticDetail
            {
                ServiceOrderId = id,
                Description = detail.Description,
                Observation = detail.Observation,
                EstimatedCost = detail.EstimatedCost,
                Priority = detail.Priority
            };
            await _unitOfWork.Repository<DiagnosticDetail>().AddAsync(diagnosticDetail, cancellationToken);
        }

        // Update order details
        var existingDetails = await _unitOfWork.Repository<OrderDetail>()
            .FindAsync(d => d.ServiceOrderId == id, cancellationToken);
        
        foreach (var detail in existingDetails)
        {
            await _unitOfWork.Repository<OrderDetail>().DeleteAsync(detail, cancellationToken);
        }

        foreach (var detail in updateDto.OrderDetails)
        {
            var orderDetail = new OrderDetail
            {
                ServiceOrderId = id,
                SpareId = detail.SpareId,
                Quantity = detail.Quantity,
                UnitPrice = detail.UnitPrice,
                Discount = detail.Discount,
                Total = detail.Quantity * detail.UnitPrice * (1 - (detail.Discount ?? 0) / 100)
            };
            await _unitOfWork.Repository<OrderDetail>().AddAsync(orderDetail, cancellationToken);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var updatedOrder = await _unitOfWork.ServiceOrdersExtended.GetByIdWithDetailsAsync(id, cancellationToken);
        return Ok(_mapper.Map<ServiceOrderDto>(updatedOrder));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteServiceOrder(int id, CancellationToken cancellationToken)
    {
        var serviceOrder = await _unitOfWork.ServiceOrders.GetByIdAsync(id, cancellationToken);
        if (serviceOrder == null) return NotFound();

        await _unitOfWork.ServiceOrders.DeleteAsync(serviceOrder, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
} 