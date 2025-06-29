namespace Application.Common.DTOs.Spares;

/// <summary>
/// DTO para la creación de nuevos repuestos en el inventario.
/// Contiene los campos necesarios para registrar un nuevo repuesto.
/// </summary>
public class CreateSpareDto
{
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
    /// Stock inicial del repuesto
    /// </summary>
    public int StockQuantity { get; set; }

    /// <summary>
    /// Cantidad mínima requerida en inventario
    /// </summary>
    public int MinimumStock { get; set; }
} 