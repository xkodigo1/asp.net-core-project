namespace Application.Common.DTOs.Invoices;

/// <summary>
/// DTO para la creación de nuevas facturas en el sistema.
/// Contiene los campos necesarios para generar una factura a partir de una orden de servicio.
/// </summary>
public class CreateInvoiceDto
{
    /// <summary>
    /// ID de la orden de servicio a facturar
    /// </summary>
    public int ServiceOrderId { get; set; }

    /// <summary>
    /// Número de factura
    /// </summary>
    public string InvoiceNumber { get; set; } = string.Empty;

    /// <summary>
    /// Fecha de emisión de la factura
    /// </summary>
    public DateTime IssueDate { get; set; }

    /// <summary>
    /// Fecha de vencimiento para el pago
    /// Si no se especifica, se calculará según las políticas del taller
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
    /// Porcentaje de descuento a aplicar
    /// </summary>
    public decimal DiscountRate { get; set; }

    /// <summary>
    /// Notas o comentarios adicionales para la factura
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