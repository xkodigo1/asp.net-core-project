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
    /// Fecha de vencimiento para el pago
    /// Si no se especifica, se calculará según las políticas del taller
    /// </summary>
    public DateTime? DueDate { get; set; }

    /// <summary>
    /// Método de pago a utilizar
    /// </summary>
    public string PaymentMethod { get; set; } = string.Empty;

    /// <summary>
    /// Porcentaje de impuesto a aplicar
    /// Si no se especifica, se usará el valor predeterminado del sistema
    /// </summary>
    public decimal? TaxRate { get; set; }

    /// <summary>
    /// Notas o comentarios adicionales para la factura
    /// </summary>
    public string? Notes { get; set; }
} 