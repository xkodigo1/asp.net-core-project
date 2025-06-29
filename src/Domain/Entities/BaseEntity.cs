namespace Domain.Entities;

/// <summary>
/// Clase base para todas las entidades del sistema.
/// Proporciona propiedades comunes para auditoría y soft delete.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Identificador único de la entidad
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Fecha y hora de creación del registro
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Usuario que creó el registro
    /// </summary>
    public string CreatedBy { get; set; } = string.Empty;

    /// <summary>
    /// Fecha y hora de la última actualización del registro
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Usuario que realizó la última actualización
    /// </summary>
    public string? UpdatedBy { get; set; }

    /// <summary>
    /// Indica si el registro está marcado como eliminado (soft delete)
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Fecha y hora en que se eliminó el registro
    /// </summary>
    public DateTime? DeletedAt { get; set; }

    /// <summary>
    /// Usuario que eliminó el registro
    /// </summary>
    public string? DeletedBy { get; set; }
} 