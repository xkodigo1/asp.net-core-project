namespace Application.Common.DTOs.Inventory;

public class CreateInventoryDto
{
    public int SpareId { get; set; }
    public string MovementType { get; set; } = string.Empty; // "IN" or "OUT"
    public int Quantity { get; set; }
    public decimal UnitCost { get; set; }
    public string BatchNumber { get; set; } = string.Empty;
    public DateTime? ExpirationDate { get; set; }
    public string Location { get; set; } = string.Empty;
    public string? Notes { get; set; }
}

public class CreateInventoryDetailDto
{
    public decimal UnitCost { get; set; }
    public int Quantity { get; set; }
    public string? BatchNumber { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public string? Location { get; set; }
} 