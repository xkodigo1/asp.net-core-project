namespace Domain.Entities;

public class Spare : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string? SerialNumber { get; set; }
    public decimal UnitPrice { get; set; }
    public int StockQuantity { get; set; }
    public int MinimumStock { get; set; }

    // Navigation properties
    public ICollection<OrderDetail> OrderDetails { get; private set; } = new List<OrderDetail>();
    public ICollection<InventoryDetail> InventoryDetails { get; private set; } = new List<InventoryDetail>();
} 