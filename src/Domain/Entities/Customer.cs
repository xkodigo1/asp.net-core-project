using System.Collections.ObjectModel;

namespace Domain.Entities;

/// <summary>
/// Representa un cliente del taller automotriz.
/// Contiene información personal y comercial del cliente, así como sus vehículos asociados.
/// </summary>
public class Customer : BaseEntity
{
    /// <summary>
    /// Nombre del cliente
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Apellidos del cliente
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Correo electrónico del cliente (debe ser único)
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
    
    #region Navigation Properties

    /// <summary>
    /// Colección de vehículos que pertenecen al cliente
    /// </summary>
    public ICollection<Vehicle> Vehicles { get; private set; } = new List<Vehicle>();

    /// <summary>
    /// Historial de órdenes de servicio del cliente
    /// </summary>
    public ICollection<ServiceOrder> ServiceOrders { get; private set; } = new List<ServiceOrder>();

    #endregion
} 