using Application.Common.DTOs.ServiceOrders;

namespace Application.Common.DTOs.Invoices;

/// <summary>
/// DTO que representa una factura en el sistema.
/// Contiene información completa de la facturación, incluyendo detalles de servicios,
/// repuestos, impuestos y totales.
/// </summary>
public class InvoiceDto
{
    /// <summary>
    /// Identificador único de la factura
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Número de factura (formato según requisitos fiscales)
    /// </summary>
    public string InvoiceNumber { get; set; } = string.Empty;

    /// <summary>
    /// ID de la orden de servicio relacionada
    /// </summary>
    public int ServiceOrderId { get; set; }

    /// <summary>
    /// Fecha de emisión de la factura
    /// </summary>
    public DateTime IssueDate { get; set; }

    /// <summary>
    /// Fecha de vencimiento para el pago
    /// </summary>
    public DateTime DueDate { get; set; }

    /// <summary>
    /// Subtotal antes de impuestos
    /// </summary>
    public decimal Subtotal { get; set; }

    /// <summary>
    /// Porcentaje de impuesto aplicado
    /// </summary>
    public decimal TaxRate { get; set; }

    /// <summary>
    /// Monto de impuesto calculado
    /// </summary>
    public decimal TaxAmount { get; set; }

    /// <summary>
    /// Monto total incluyendo impuestos
    /// </summary>
    public decimal Total { get; set; }

    /// <summary>
    /// Estado de la factura (Pendiente, Pagada, Anulada, etc.)
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Método de pago utilizado
    /// </summary>
    public string PaymentMethod { get; set; } = string.Empty;

    /// <summary>
    /// Notas o comentarios adicionales en la factura
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Información completa de la orden de servicio facturada
    /// </summary>
    public ServiceOrderDto ServiceOrder { get; set; } = null!;

    /// <summary>
    /// Fecha y hora de creación del registro
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Usuario que creó el registro
    /// </summary>
    public string CreatedBy { get; set; } = string.Empty;

    /// <summary>
    /// Fecha y hora de la última actualización
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Usuario que realizó la última actualización
    /// </summary>
    public string? UpdatedBy { get; set; }
} 