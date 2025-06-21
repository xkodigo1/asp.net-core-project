namespace Domain.Entities;

public class OrderDetail : BaseEntity
{
    public Guid ServiceOrderId { get; set; }
    public Guid SpareId { get; set; }
    public int Quantity { get; set; }
    public decimal TotalCost { get; set; }

    // Navigation properties
    public ServiceOrder ServiceOrder { get; set; } = null!;
    public Spare Spare { get; set; } = null!;
} 