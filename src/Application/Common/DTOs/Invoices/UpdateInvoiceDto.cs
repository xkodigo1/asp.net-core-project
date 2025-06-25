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
    /// Nuevo estado de la factura (ej: Pagada, Anulada)
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Nueva fecha de vencimiento para el pago
    /// </summary>
    public DateTime? DueDate { get; set; }

    /// <summary>
    /// Nuevo método de pago
    /// </summary>
    public string? PaymentMethod { get; set; }

    /// <summary>
    /// Notas o comentarios actualizados
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Fecha de pago efectivo
    /// Se establece cuando la factura se marca como pagada
    /// </summary>
    public DateTime? PaymentDate { get; set; }

    /// <summary>
    /// Referencia del pago (número de transacción, cheque, etc.)
    /// </summary>
    public string? PaymentReference { get; set; }
} 