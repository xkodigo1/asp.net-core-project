namespace Application.Common.DTOs.Spares;

/// <summary>
/// DTO para la actualización de repuestos existentes en el inventario.
/// Permite modificar los detalles y configuración de un repuesto.
/// </summary>
public class UpdateSpareDto
{
    /// <summary>
    /// Identificador del repuesto
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre del repuesto
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Descripción detallada del repuesto
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Marca del repuesto
    /// </summary>
    public string Brand { get; set; } = string.Empty;

    /// <summary>
    /// Modelo o referencia del repuesto
    /// </summary>
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// Número de serie del repuesto (si aplica)
    /// </summary>
    public string? SerialNumber { get; set; }

    /// <summary>
    /// Precio unitario del repuesto
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Cantidad actual en inventario
    /// </summary>
    public int StockQuantity { get; set; }

    /// <summary>
    /// Cantidad mínima requerida en inventario
    /// </summary>
    public int MinimumStock { get; set; }
} 