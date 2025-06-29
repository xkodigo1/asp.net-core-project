namespace Domain.Entities;

public class InventoryDetail : BaseEntity
{
    public int InventoryId { get; set; }
    public int SpareId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitCost { get; set; }
    public decimal Total { get; set; }
    public string BatchNumber { get; set; } = string.Empty;
    public DateTime? ExpirationDate { get; set; }
    public string Location { get; set; } = string.Empty;

    // Navigation properties
    public Inventory Inventory { get; set; } = null!;
    public Spare Spare { get; set; } = null!;
} 