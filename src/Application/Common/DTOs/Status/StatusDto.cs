namespace Application.Common.DTOs.Status;

/// <summary>
/// DTO que representa un estado posible para las órdenes de servicio.
/// Permite rastrear el progreso de las órdenes a través del flujo de trabajo del taller.
/// </summary>
public class StatusDto
{
    /// <summary>
    /// Identificador único del estado
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre del estado
    /// (ej: En Espera, En Proceso, Finalizado, Cancelado)
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Descripción detallada del significado del estado
    /// y las implicaciones para el flujo de trabajo
    /// </summary>
    public string? Description { get; set; }
} 