namespace Application.Common.DTOs.Diagnostics;

/// <summary>
/// DTO para la creación de detalles específicos dentro de un diagnóstico.
/// Permite registrar cada problema o condición encontrada durante la inspección del vehículo.
/// </summary>
public class CreateDiagnosticDetailDto
{
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
    /// Notas adicionales sobre este detalle específico
    /// </summary>
    public string? Notes { get; set; }
} 