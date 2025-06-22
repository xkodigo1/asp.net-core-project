namespace Domain.Entities;

public class InventoryDetail : BaseEntity
{
    public int ServiceOrderId { get; set; }
    public int InventoryId { get; set; }
    public int SpareId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitCost { get; set; }
    public decimal Total { get; set; }

    // Navigation properties
    public ServiceOrder ServiceOrder { get; set; } = null!;
    public Inventory Inventory { get; set; } = null!;
    public Spare Spare { get; set; } = null!;
} 