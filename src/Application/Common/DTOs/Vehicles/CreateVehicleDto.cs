namespace Application.Common.DTOs.Vehicles;

/// <summary>
/// DTO para la creación de nuevos vehículos en el sistema.
/// Contiene solo los campos necesarios para registrar un vehículo,
/// excluyendo campos calculados o que se generan automáticamente.
/// </summary>
public class CreateVehicleDto
{
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
    /// Kilometraje inicial del vehículo al momento del registro
    /// </summary>
    public decimal Mileage { get; set; }

    /// <summary>
    /// Número de motor del vehículo
    /// Identificador único del motor asignado por el fabricante
    /// </summary>
    public string EngineNumber { get; set; } = string.Empty;

    /// <summary>
    public string? Notes { get; set; }
} 