namespace Application.Common.DTOs.Vehicles;

public class CreateVehicleDto
{
    public int CustomerId { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string VinNumber { get; set; } = string.Empty;
    public int Mileage { get; set; }
} 