namespace Application.Common.DTOs.Users;

/// <summary>
/// DTO para actualizar usuarios existentes en el sistema.
/// Permite modificar información personal y asignaciones de roles/especializaciones.
/// Los campos son opcionales para permitir actualizaciones parciales.
/// </summary>
public class UpdateUserDto
{
    /// <summary>
    /// ID del usuario a actualizar
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nuevo nombre de usuario (opcional)
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// Nuevo nombre del usuario (opcional)
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Nuevos apellidos del usuario (opcional)
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Nuevo correo electrónico del usuario (opcional)
    /// Debe ser único si se proporciona
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Nueva contraseña del usuario (opcional)
    /// Si se proporciona, debe cumplir con los requisitos de seguridad
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// Nuevo número de teléfono del usuario (opcional)
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// Nueva dirección del usuario (opcional)
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Nuevo número de identificación personal (opcional)
    /// </summary>
    public string? Identification { get; set; }

    /// <summary>
    /// Nueva lista de IDs de roles (opcional)
    /// Si se proporciona, reemplazará completamente la lista actual de roles
    /// </summary>
    public List<int> RoleIds { get; set; } = new();

    /// <summary>
    /// Nueva lista de IDs de especializaciones (opcional)
    /// Si se proporciona, reemplazará completamente la lista actual de especializaciones
    /// </summary>
    public List<int> SpecializationIds { get; set; } = new();

    /// <summary>
    /// Indica si el usuario debe estar activo o inactivo
    /// </summary>
    public bool IsActive { get; set; }
} 