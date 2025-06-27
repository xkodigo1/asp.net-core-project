namespace Application.Common.DTOs.Inventory;

/// <summary>
/// DTO para la creación de nuevos ítems en el inventario.
/// Contiene los campos necesarios para registrar un nuevo repuesto en el sistema.
/// </summary>
public class CreateInventoryDto
{
    /// <summary>
    /// Código único del repuesto (SKU o código interno)
    /// Debe ser único en el sistema
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Nombre o descripción corta del repuesto
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Descripción detallada del repuesto
    /// Puede incluir especificaciones técnicas y compatibilidad
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
    /// Cantidad inicial en stock
    /// </summary>
    public int InitialStock { get; set; }

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
    /// Notas adicionales sobre el repuesto
    /// </summary>
    public string? Notes { get; set; }

    public int SpareId { get; set; }
    public int Quantity { get; set; }
    public string DocumentNumber { get; set; } = string.Empty;
    public string DocumentType { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string? BatchNumber { get; set; }
    public DateTime? ExpirationDate { get; set; }
}

public class CreateInventoryDetailDto
{
    public decimal UnitCost { get; set; }
    public int Quantity { get; set; }
    public string? BatchNumber { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public string? Location { get; set; }
} 