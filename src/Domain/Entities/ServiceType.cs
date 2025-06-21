using AutoTallerManager.Domain.Entities;

namespace Domain.Entities;

public class ServiceType : BaseEntity
{
    public int Duration { get; set; }
    public string Description { get; set; } = string.Empty;

    // Navigation properties
    public ICollection<ServiceOrder> ServiceOrders { get; private set; } = new List<ServiceOrder>();
} 