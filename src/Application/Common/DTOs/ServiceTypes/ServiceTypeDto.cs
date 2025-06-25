namespace Application.Common.DTOs.ServiceTypes;

/// <summary>
/// DTO que representa un tipo de servicio ofrecido por el taller.
/// Define las características básicas y costos base de cada tipo de servicio disponible.
/// </summary>
public class ServiceTypeDto
{
    /// <summary>
    /// Identificador único del tipo de servicio
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre del tipo de servicio
    /// (ej: Mantenimiento Preventivo, Reparación Mayor, Diagnóstico General)
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Descripción detallada del tipo de servicio
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Tiempo estimado para realizar el servicio (en horas)
    /// </summary>
    public int EstimatedTime { get; set; }

    /// <summary>
    /// Precio base del servicio, sin incluir repuestos
    /// </summary>
    public decimal BasePrice { get; set; }
} 