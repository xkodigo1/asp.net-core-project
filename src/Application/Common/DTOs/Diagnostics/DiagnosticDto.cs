using Application.Common.DTOs.Users;

namespace Application.Common.DTOs.Diagnostics;

/// <summary>
/// DTO que representa un diagnóstico técnico realizado a un vehículo.
/// Contiene la evaluación detallada del estado del vehículo y las recomendaciones del mecánico.
/// </summary>
public class DiagnosticDto
{
    /// <summary>
    /// Identificador único del diagnóstico
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ID de la orden de servicio asociada
    /// </summary>
    public int ServiceOrderId { get; set; }

    /// <summary>
    /// Información del mecánico que realizó el diagnóstico
    /// </summary>
    public UserDto Mechanic { get; set; } = null!;

    /// <summary>
    /// Fecha y hora en que se realizó el diagnóstico
    /// </summary>
    public DateTime DiagnosticDate { get; set; }

    /// <summary>
    /// Descripción general del estado del vehículo
    /// </summary>
    public string GeneralCondition { get; set; } = string.Empty;

    /// <summary>
    /// Lista de problemas encontrados durante la inspección
    /// </summary>
    public ICollection<DiagnosticDetailDto> DiagnosticDetails { get; set; } = new List<DiagnosticDetailDto>();

    /// <summary>
    /// Recomendaciones generales del mecánico
    /// </summary>
    public string Recommendations { get; set; } = string.Empty;

    /// <summary>
    /// Nivel de urgencia del servicio (Alto, Medio, Bajo)
    /// </summary>
    public string UrgencyLevel { get; set; } = string.Empty;

    /// <summary>
    /// Kilometraje del vehículo al momento del diagnóstico
    /// </summary>
    public decimal CurrentMileage { get; set; }

    /// <summary>
    /// Notas adicionales sobre el diagnóstico
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

    /// <summary>
    /// Fecha y hora de la última actualización
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Usuario que realizó la última actualización
    /// </summary>
    public string? UpdatedBy { get; set; }
} 