using Application.Common.DTOs.Spares;
 

namespace Application.Common.DTOs.Inventory;

/// <summary>
/// DTO que representa un ítem en el inventario del taller.
/// Contiene información sobre repuestos, cantidades, costos y movimientos.
/// </summary>

public class InventoryDto
{
    /// <summary>
    /// Identificador único del ítem de inventario
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Código único del repuesto (SKU o código interno)
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Nombre o descripción del repuesto
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Descripción detallada del repuesto
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Marca del repuesto
    /// </summary>
    public string Brand { get; set; } = string.Empty;

    /// <summary>
    /// Modelo o referencia del repuesto
    /// </summary>
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// Cantidad actual en stock
    /// </summary>
    public int Stock { get; set; }

    /// <summary>
    /// Cantidad mínima permitida antes de necesitar reabastecimiento
    /// </summary>
    public int MinimumStock { get; set; }

    /// <summary>
    /// Costo unitario de compra del repuesto
    /// </summary>
    public decimal UnitCost { get; set; }

    /// <summary>
    /// Precio de venta unitario del repuesto
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Ubicación física del repuesto en el almacén
    /// </summary>
    public string Location { get; set; } = string.Empty;

    /// <summary>
    /// Categoría o tipo de repuesto
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Indica si el repuesto está activo para su uso
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Lista de movimientos de inventario relacionados con este repuesto
    /// </summary>
    public ICollection<InventoryDetailDto> InventoryDetails { get; set; } = new List<InventoryDetailDto>();

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

