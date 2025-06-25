namespace Application.Common.DTOs.Spares;

/// <summary>
/// DTO que representa un repuesto o pieza de recambio en el inventario.
/// Contiene la información completa de un repuesto, incluyendo detalles de stock y auditoría.
/// </summary>
public class SpareDto
{
    /// <summary>
    /// Identificador único del repuesto
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
    /// Precio de venta del repuesto
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Cantidad actual en inventario
    /// </summary>
    public int Stock { get; set; }

    /// <summary>
    /// Cantidad mínima requerida en inventario
    /// </summary>
    public int MinimumStock { get; set; }

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