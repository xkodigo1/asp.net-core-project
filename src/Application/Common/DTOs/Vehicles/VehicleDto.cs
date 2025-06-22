using Application.Common.DTOs.Customers;

namespace Application.Common.DTOs.Vehicles;

public class VehicleDto
{
    public int Id { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string VinNumber { get; set; } = string.Empty;
    public int Mileage { get; set; }
    public CustomerDto Owner { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
} 