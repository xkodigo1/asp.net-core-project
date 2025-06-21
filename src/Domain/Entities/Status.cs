namespace Domain.Entities;

public class Status : BaseEntity
{
    public string StatusType { get; set; } = string.Empty;

    // Navigation properties
    public ICollection<ServiceOrder> ServiceOrders { get; private set; } = new List<ServiceOrder>();
} 