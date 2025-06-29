using Application.Common.DTOs.Invoices;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class InvoicesController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public InvoicesController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InvoiceDto>>> GetInvoices(CancellationToken cancellationToken)
    {
        var invoices = await _unitOfWork.Invoices.GetAllAsync(cancellationToken);
        return Ok(_mapper.Map<IEnumerable<InvoiceDto>>(invoices));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InvoiceDto>> GetInvoice(int id, CancellationToken cancellationToken)
    {
        var invoice = await _unitOfWork.Invoices.GetByIdAsync(id, cancellationToken);
        if (invoice == null) return NotFound();

        return Ok(_mapper.Map<InvoiceDto>(invoice));
    }

    [HttpGet("by-date-range")]
    public async Task<ActionResult<IEnumerable<InvoiceDto>>> GetByDateRange(
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate,
        CancellationToken cancellationToken)
    {
        var invoices = await _unitOfWork.Repository<Invoice>()
            .FindAsync(i => 
                i.Date >= startDate && 
                i.Date <= endDate && 
                !i.IsDeleted, 
                cancellationToken);

        return Ok(_mapper.Map<IEnumerable<InvoiceDto>>(invoices));
    }

    [HttpGet("by-customer/{customerId}")]
    public async Task<ActionResult<IEnumerable<InvoiceDto>>> GetByCustomer(int customerId, CancellationToken cancellationToken)
    {
        var invoices = await _unitOfWork.Repository<Invoice>()
            .FindAsync(i => 
                i.ServiceOrder.Vehicle.CustomerId == customerId && 
                !i.IsDeleted, 
                cancellationToken);

        return Ok(_mapper.Map<IEnumerable<InvoiceDto>>(invoices));
    }

    [HttpPost]
    public async Task<ActionResult<InvoiceDto>> CreateInvoice(CreateInvoiceDto createDto, CancellationToken cancellationToken)
    {
        // Validar que la orden de servicio existe
        var serviceOrder = await _unitOfWork.ServiceOrdersExtended
            .GetByIdWithDetailsAsync(createDto.ServiceOrderId, cancellationToken);

        if (serviceOrder == null)
            return BadRequest("La orden de servicio especificada no existe.");

        // Validar que la orden no tenga ya una factura
        if (serviceOrder.Invoice != null)
            return BadRequest("La orden de servicio ya tiene una factura asociada.");

        // Calcular el total de la orden
        decimal total = 0;
        foreach (var detail in serviceOrder.OrderDetails)
        {
            total += detail.Total;
        }

        // Aplicar impuestos y descuentos
        decimal subtotal = total;
        decimal tax = subtotal * (createDto.TaxRate / 100);
        decimal discount = subtotal * (createDto.DiscountRate / 100);
        decimal finalTotal = subtotal + tax - discount;

        var invoice = new Invoice
        {
            ServiceOrderId = createDto.ServiceOrderId,
            Number = DateTime.Now.ToString("yyyyMMddHHmmss"),
            Date = DateTime.Now,
            Subtotal = subtotal,
            TaxRate = createDto.TaxRate,
            TaxAmount = tax,
            DiscountRate = createDto.DiscountRate,
            DiscountAmount = discount,
            Total = finalTotal,
            Notes = createDto.Notes,
            PaymentMethod = createDto.PaymentMethod,
            PaymentStatus = "PENDIENTE"
        };

        await _unitOfWork.Invoices.AddAsync(invoice, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return CreatedAtAction(nameof(GetInvoice), new { id = invoice.Id }, _mapper.Map<InvoiceDto>(invoice));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<InvoiceDto>> UpdateInvoice(int id, UpdateInvoiceDto updateDto, CancellationToken cancellationToken)
    {
        var invoice = await _unitOfWork.Invoices.GetByIdAsync(id, cancellationToken);
        if (invoice == null) return NotFound();

        // Solo permitir actualizar notas y estado de pago
        invoice.Notes = updateDto.Notes ?? invoice.Notes;
        invoice.PaymentStatus = updateDto.PaymentStatus;
        invoice.PaymentMethod = updateDto.PaymentMethod ?? invoice.PaymentMethod;

        await _unitOfWork.Invoices.UpdateAsync(invoice, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Ok(_mapper.Map<InvoiceDto>(invoice));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInvoice(int id, CancellationToken cancellationToken)
    {
        var invoice = await _unitOfWork.Invoices.GetByIdAsync(id, cancellationToken);
        if (invoice == null) return NotFound();

        // Solo permitir eliminar facturas pendientes de pago
        if (invoice.PaymentStatus != "PENDIENTE")
            return BadRequest("No se puede eliminar una factura que ya ha sido pagada.");

        await _unitOfWork.Invoices.SoftDeleteAsync(invoice, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
} 