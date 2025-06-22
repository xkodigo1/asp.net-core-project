namespace Application.Common.DTOs.ServiceOrders;

public class CreateServiceOrderDto
{
    public int VehicleId { get; set; }
    public int MechanicId { get; set; }
    public int ServiceTypeId { get; set; }
    public DateTime EntryDate { get; set; }
    public DateTime? ExitDate { get; set; }
    public string? CustomerMessage { get; set; }
    public List<CreateDiagnosticDetailDto> DiagnosticDetails { get; set; } = new();
    public List<CreateOrderDetailDto> OrderDetails { get; set; } = new();
}

public class CreateDiagnosticDetailDto
{
    public string Description { get; set; } = string.Empty;
    public string? Observation { get; set; }
    public decimal EstimatedCost { get; set; }
    public int Priority { get; set; }
}

public class CreateOrderDetailDto
{
    public int SpareId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal? Discount { get; set; }
} 