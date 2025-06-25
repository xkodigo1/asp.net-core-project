namespace Application.Common.DTOs.Diagnostics;

/// <summary>
/// DTO para la creación de nuevos diagnósticos en el sistema.
/// Contiene los campos necesarios para registrar la evaluación inicial de un vehículo.
/// </summary>
public class CreateDiagnosticDto
{
    /// <summary>
    /// ID de la orden de servicio asociada
    /// </summary>
    public int ServiceOrderId { get; set; }

    /// <summary>
    /// ID del mecánico que realiza el diagnóstico
    /// </summary>
    public int MechanicId { get; set; }

    /// <summary>
    /// Descripción general del estado del vehículo
    /// </summary>
    public string GeneralCondition { get; set; } = string.Empty;

    /// <summary>
    /// Lista de problemas encontrados durante la inspección
    /// </summary>
    public List<CreateDiagnosticDetailDto> DiagnosticDetails { get; set; } = new();

    /// <summary>
    /// Recomendaciones generales del mecánico
    /// </summary>
    public string Recommendations { get; set; } = string.Empty;

    /// <summary>
    /// Nivel de urgencia del servicio (Alto, Medio, Bajo)
    /// </summary>
    public string UrgencyLevel { get; set; } = string.Empty;

    /// <summary>
    /// Kilometraje actual del vehículo
    /// </summary>
    public decimal CurrentMileage { get; set; }

    /// <summary>
    /// Notas adicionales sobre el diagnóstico
    /// </summary>
    public string? Notes { get; set; }
} 