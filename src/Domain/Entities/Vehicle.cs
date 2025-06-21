using AutoTallerManager.Domain.Entities;

namespace Domain.Entities;

public class Vehicle : BaseEntity
{
    public int CustomerId { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string VinNumber { get; set; } = string.Empty;
    public int Mileage { get; set; }

    // Navigation properties
    public Customer Customer { get; set; } = null!;
    public ICollection<ServiceOrder> ServiceOrders { get; private set; } = new List<ServiceOrder>();
} 