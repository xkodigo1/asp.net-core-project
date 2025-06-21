namespace Domain.Entities;

public class Inventory : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    // Navigation properties
    public ICollection<InventoryDetail> InventoryDetails { get; private set; } = new List<InventoryDetail>();
} 