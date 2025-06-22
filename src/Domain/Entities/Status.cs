namespace Domain.Entities;

public class Status : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    // Navigation properties
    public ICollection<ServiceOrder> ServiceOrders { get; private set; } = new List<ServiceOrder>();
} 