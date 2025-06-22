namespace Application.Common.DTOs.ServiceOrders;

public class UpdateServiceOrderDto
{
    public int Id { get; set; }
    public int VehicleId { get; set; }
    public int MechanicId { get; set; }
    public int ServiceTypeId { get; set; }
    public int StatusId { get; set; }
    public DateTime EntryDate { get; set; }
    public DateTime? ExitDate { get; set; }
    public string? CustomerMessage { get; set; }
    public List<UpdateDiagnosticDetailDto> DiagnosticDetails { get; set; } = new();
    public List<UpdateOrderDetailDto> OrderDetails { get; set; } = new();
}

public class UpdateDiagnosticDetailDto
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public string? Observation { get; set; }
    public decimal EstimatedCost { get; set; }
    public int Priority { get; set; }
}

public class UpdateOrderDetailDto
{
    public int Id { get; set; }
    public int SpareId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal? Discount { get; set; }
} 