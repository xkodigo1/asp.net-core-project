namespace Application.Common.DTOs.Inventory;

/// <summary>
/// DTO que representa un movimiento en el inventario.
/// Registra entradas, salidas y ajustes de inventario con su respectiva información.
/// </summary>
public class InventoryDetailDto
{
    /// <summary>
    /// Identificador único del movimiento
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ID del ítem de inventario relacionado
    /// </summary>
    public int InventoryId { get; set; }

    /// <summary>
    /// Tipo de movimiento (Entrada, Salida, Ajuste)
    /// </summary>
    public string MovementType { get; set; } = string.Empty;

    /// <summary>
    /// Cantidad de unidades movidas
    /// Positivo para entradas, negativo para salidas
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Costo unitario del repuesto en el momento del movimiento
    /// </summary>
    public decimal UnitCost { get; set; }

    /// <summary>
    /// Número de lote del repuesto (si aplica)
    /// </summary>
    public string? BatchNumber { get; set; }

    /// <summary>
    /// Fecha de vencimiento del lote (si aplica)
    /// </summary>
    public DateTime? ExpirationDate { get; set; }

    /// <summary>
    /// Referencia del documento que originó el movimiento
    /// (Número de factura, orden de compra, etc.)
    /// </summary>
    public string? Reference { get; set; }

    /// <summary>
    /// Notas o comentarios sobre el movimiento
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Fecha y hora de creación del registro
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Usuario que creó el registro
    /// </summary>
    public string CreatedBy { get; set; } = string.Empty;
} 