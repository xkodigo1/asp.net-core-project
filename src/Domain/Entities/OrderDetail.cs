namespace Domain.Entities;

public class OrderDetail : BaseEntity
{
    public int ServiceOrderId { get; set; }
    public int SpareId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal? Discount { get; set; }
    public decimal Total { get; set; }

    // Navigation properties
    public ServiceOrder ServiceOrder { get; set; } = null!;
    public Spare Spare { get; set; } = null!;
} 