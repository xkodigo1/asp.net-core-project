namespace Application.Common.DTOs.Users;

/// <summary>
/// DTO para la creación de nuevos usuarios en el sistema.
/// Contiene los campos necesarios para registrar un usuario,
/// incluyendo información personal y credenciales de acceso.
/// </summary>
public class CreateUserDto
{
    /// <summary>
    /// Nombre de usuario para el sistema
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Contraseña del usuario (debe cumplir con los requisitos de seguridad)
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Nombre del usuario
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Apellidos del usuario
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Correo electrónico del usuario (debe ser único)
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
    /// Lista de IDs de roles a asignar al usuario
    /// </summary>
    public List<int> RoleIds { get; set; } = new();

    /// <summary>
    /// Lista de IDs de especializaciones a asignar (solo para mecánicos)
    /// </summary>
    public List<int> SpecializationIds { get; set; } = new();
} 