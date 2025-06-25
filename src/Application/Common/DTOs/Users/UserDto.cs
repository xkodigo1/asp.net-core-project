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

/// <summary>
/// DTO que representa un rol en el sistema.
/// Define los diferentes niveles de acceso y responsabilidades de los usuarios.
/// </summary>
public class RoleDto
{
    /// <summary>
    /// Identificador único del rol
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre del rol
    /// (ej: Administrador, Mecánico, Recepcionista)
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Descripción detallada de las responsabilidades
    /// y permisos asociados al rol
    /// </summary>
    public string? Description { get; set; }
}

/// <summary>
/// DTO que representa una especialización técnica en el taller.
/// Define las diferentes áreas de experiencia y capacitación de los mecánicos.
/// </summary>
public class SpecializationDto
{
    /// <summary>
    /// Identificador único de la especialización
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre de la especialización
    /// (ej: Mecánica General, Electricidad, Sistemas de Inyección)
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Descripción detallada del área de especialización
    /// y las habilidades requeridas
    /// </summary>
    public string? Description { get; set; }
} 