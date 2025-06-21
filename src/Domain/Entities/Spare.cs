namespace Domain.Entities;

public class Spare : BaseEntity
{
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int StockQuantity { get; set; }
    public int MinimumStock { get; set; }
    public decimal UnitPrice { get; set; }
    public string Category { get; set; } = string.Empty;

    // Navigation properties
    public ICollection<OrderDetail> OrderDetails { get; private set; } = new List<OrderDetail>();
} 