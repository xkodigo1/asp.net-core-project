namespace Application.Common.DTOs.Diagnostics;

/// <summary>
/// DTO que representa un detalle específico dentro de un diagnóstico.
/// Cada detalle corresponde a un problema o condición encontrada durante la inspección.
/// </summary>
public class DiagnosticDetailDto
{
    /// <summary>
    /// Identificador único del detalle de diagnóstico
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ID del diagnóstico principal al que pertenece este detalle
    /// </summary>
    public int DiagnosticId { get; set; }

    /// <summary>
    /// Sistema o componente del vehículo afectado
    /// (ej: Motor, Frenos, Suspensión, etc.)
    /// </summary>
    public string Component { get; set; } = string.Empty;

    /// <summary>
    /// Descripción detallada del problema o condición encontrada
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Nivel de severidad del problema (Crítico, Moderado, Leve)
    /// </summary>
    public string SeverityLevel { get; set; } = string.Empty;

    /// <summary>
    /// Recomendación específica para este problema
    /// </summary>
    public string Recommendation { get; set; } = string.Empty;

    /// <summary>
    /// Costo estimado de la reparación
    /// </summary>
    public decimal EstimatedCost { get; set; }

    /// <summary>
    /// Tiempo estimado para la reparación (en horas)
    /// </summary>
    public decimal EstimatedTime { get; set; }

    /// <summary>
    /// Indica si el cliente aprobó la reparación de este problema
    /// </summary>
    public bool IsApproved { get; set; }

    /// <summary>
    /// Notas adicionales sobre este detalle específico
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