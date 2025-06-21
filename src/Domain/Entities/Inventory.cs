namespace Domain.Entities;

public class Inventory : BaseEntity
{
    public string DocumentNumber { get; set; } = string.Empty;
    public string DocumentType { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string? Notes { get; set; }

    // Navigation properties
    public ICollection<InventoryDetail> InventoryDetails { get; private set; } = new List<InventoryDetail>();
} 