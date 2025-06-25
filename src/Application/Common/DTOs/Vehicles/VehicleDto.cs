using Application.Common.DTOs.Customers;
using Application.Common.DTOs.ServiceOrders;

namespace Application.Common.DTOs.Vehicles;

/// <summary>
/// DTO que representa un vehículo en el sistema.
/// Contiene información detallada del vehículo, su propietario y su historial de servicios.
/// </summary>
public class VehicleDto
{
    /// <summary>
    /// Identificador único del vehículo
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ID del cliente propietario del vehículo
    /// </summary>
    public int CustomerId { get; set; }

    /// <summary>
    /// Marca del vehículo (ej: Toyota, Honda, Ford)
    /// </summary>
    public string Brand { get; set; } = string.Empty;

    /// <summary>
    /// Modelo del vehículo
    /// </summary>
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// Año de fabricación del vehículo
    /// </summary>
    public int Year { get; set; }

    /// <summary>
    /// Número de identificación del vehículo (VIN)
    /// Código único de 17 caracteres que identifica al vehículo a nivel mundial
    /// </summary>
    public string VIN { get; set; } = string.Empty;

    /// <summary>
    /// Placa o matrícula del vehículo
    /// </summary>
    public string LicensePlate { get; set; } = string.Empty;

    /// <summary>
    /// Color del vehículo
    /// </summary>
    public string Color { get; set; } = string.Empty;

    /// <summary>
    /// Kilometraje actual del vehículo
    /// </summary>
    public decimal Mileage { get; set; }

    /// <summary>
    /// Información completa del propietario del vehículo
    /// </summary>
    public CustomerDto Customer { get; set; } = null!;

    /// <summary>
    /// Historial de órdenes de servicio del vehículo
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