using Application.Common.DTOs.Spares;

namespace Application.Common.DTOs.Inventory;

public class InventoryDto
{
    public int Id { get; set; }
    public SpareDto Spare { get; set; } = null!;
    public string MovementType { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public string? Reference { get; set; }
    public string? Notes { get; set; }
    public List<InventoryDetailDto> Details { get; set; } = new();
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
}

public class InventoryDetailDto
{
    public int Id { get; set; }
    public decimal UnitCost { get; set; }
    public int Quantity { get; set; }
    public string? BatchNumber { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public string? Location { get; set; }
    public DateTime CreatedAt { get; set; }
} 