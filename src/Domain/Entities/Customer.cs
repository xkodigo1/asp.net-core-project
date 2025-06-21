using System.Collections.ObjectModel;

namespace AutoTallerManager.Domain.Entities;

public class Customer : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string? TaxId { get; set; }
    public string? CompanyName { get; set; }
    public string Identification { get; set; } = string.Empty;
    
    // Navigation properties
    public ICollection<Vehicle> Vehicles { get; private set; } = new List<Vehicle>();
    public ICollection<ServiceOrder> ServiceOrders { get; private set; } = new Collection<ServiceOrder>();
} 