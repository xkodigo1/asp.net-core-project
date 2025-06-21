namespace Domain.Entities;

public class InventoryDetail : BaseEntity
{
    public Guid ServiceOrderId { get; set; }
    public Guid InventoryId { get; set; }
    public Guid SpareId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitCost { get; set; }
    public decimal Total { get; set; }

    // Navigation properties
    public ServiceOrder ServiceOrder { get; set; } = null!;
    public Inventory Inventory { get; set; } = null!;
    public Spare Spare { get; set; } = null!;
} 