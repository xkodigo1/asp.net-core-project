using Application.Common.DTOs.ServiceOrders;
using Application.Common.DTOs.Vehicles;

namespace Application.Common.DTOs.Customers;

/// <summary>
/// DTO que representa un cliente del taller.
/// Incluye información personal, comercial y relaciones con vehículos y órdenes de servicio.
/// </summary>
public class CustomerDto
{
    /// <summary>
    /// Identificador único del cliente
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre del cliente
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Apellidos del cliente
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Nombre completo del cliente (FirstName + LastName)
    /// </summary>
    public string FullName => $"{FirstName} {LastName}";

    /// <summary>
    /// Correo electrónico del cliente
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Número de teléfono del cliente
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// Dirección física del cliente
    /// </summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Identificación fiscal del cliente (RFC, NIT, etc.)
    /// </summary>
    public string? TaxId { get; set; }

    /// <summary>
    /// Nombre de la empresa (si el cliente representa una empresa)
    /// </summary>
    public string? CompanyName { get; set; }

    /// <summary>
    /// Número de identificación personal (DNI, Cédula, etc.)
    /// </summary>
    public string Identification { get; set; } = string.Empty;

    /// <summary>
    /// Lista de vehículos que pertenecen al cliente
    /// </summary>
    public ICollection<VehicleDto> Vehicles { get; set; } = new List<VehicleDto>();

    /// <summary>
    /// Historial de órdenes de servicio del cliente
    /// </summary>
    public ICollection<ServiceOrderDto> ServiceOrders { get; set; } = new List<ServiceOrderDto>();

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