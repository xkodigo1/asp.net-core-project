namespace Application.Common.DTOs.Users;

/// <summary>
/// DTO que representa un usuario del sistema.
/// Contiene información del empleado, sus roles y especializaciones.
/// Se usa para mostrar información del usuario sin exponer datos sensibles.
/// </summary>
public class UserDto
{
    /// <summary>
    /// Identificador único del usuario
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre del usuario
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Nombre del usuario
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Apellidos del usuario
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Nombre completo del usuario (FirstName + LastName)
    /// </summary>
    public string FullName => $"{FirstName} {LastName}";

    /// <summary>
    /// Correo electrónico del usuario (usado para autenticación)
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Número de teléfono del usuario
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// Dirección del usuario
    /// </summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Número de identificación personal (DNI, Cédula, etc.)
    /// </summary>
    public string Identification { get; set; } = string.Empty;

    /// <summary>
    /// Lista de roles asignados al usuario (ej: Admin, Mecánico, Recepcionista)
    /// </summary>
    public List<RoleDto> Roles { get; set; } = new();

    /// <summary>
    /// Lista de especializaciones técnicas del usuario (solo para mecánicos)
    /// </summary>
    public List<SpecializationDto> Specializations { get; set; } = new();

    /// <summary>
    /// Indica si el usuario está activo en el sistema
    /// </summary>
    public bool IsActive { get; set; }

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

public class RoleDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class SpecializationDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
} 