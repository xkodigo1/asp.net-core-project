namespace Application.Common.DTOs.Invoices;

/// <summary>
/// DTO para actualizar facturas existentes en el sistema.
/// Permite modificar ciertos campos de la factura mientras mantiene la integridad de los datos fiscales.
/// </summary>
public class UpdateInvoiceDto
{
    /// <summary>
    /// ID de la factura a actualizar
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ID de la orden de servicio asociada a la factura
    /// </summary>
    public int ServiceOrderId { get; set; }

    /// <summary>
    /// Número de la factura
    /// </summary>
    public string InvoiceNumber { get; set; } = string.Empty;

    /// <summary>
    /// Fecha de emisión de la factura
    /// </summary>
    public DateTime IssueDate { get; set; }

    /// <summary>
    /// Nueva fecha de vencimiento para el pago
    /// </summary>
    public DateTime DueDate { get; set; }

    /// <summary>
    /// Subtotal de la factura
    /// </summary>
    public decimal Subtotal { get; set; }

    /// <summary>
    /// Porcentaje de impuesto de la factura
    /// </summary>
    public decimal TaxRate { get; set; }

    /// <summary>
    /// Impuesto de la factura
    /// </summary>
    public decimal Tax { get; set; }

    /// <summary>
    /// Total de la factura
    /// </summary>
    public decimal Total { get; set; }

    /// <summary>
    /// Porcentaje de descuento aplicado a la factura
    /// </summary>
    public decimal DiscountRate { get; set; }

    /// <summary>
    /// Notas o comentarios actualizados
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Estado de pago de la factura
    /// </summary>
    public string PaymentStatus { get; set; } = "Pending";

    /// <summary>
    /// Método de pago de la factura
    /// </summary>
    public string PaymentMethod { get; set; } = string.Empty;
} 