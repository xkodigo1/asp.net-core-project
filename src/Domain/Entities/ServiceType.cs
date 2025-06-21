namespace Domain.Entities;

public class ServiceType : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int EstimatedTime { get; set; }
    public decimal BasePrice { get; set; }

    // Navigation properties
    public ICollection<ServiceOrder> ServiceOrders { get; private set; } = new List<ServiceOrder>();
} 