using Application.Common.DTOs.Customers;
using Application.Common.DTOs.Spares;
using Application.Common.DTOs.Users;
using Application.Common.DTOs.Vehicles;

namespace Application.Common.DTOs.ServiceOrders;

public class ServiceOrderDto
{
    public int Id { get; set; }
    public VehicleDto Vehicle { get; set; } = null!;
    public UserDto Mechanic { get; set; } = null!;
    public ServiceTypeDto ServiceType { get; set; } = null!;
    public StatusDto Status { get; set; } = null!;
    public DateTime EntryDate { get; set; }
    public DateTime? ExitDate { get; set; }
    public string? CustomerMessage { get; set; }
    public List<DiagnosticDetailDto> DiagnosticDetails { get; set; } = new();
    public List<OrderDetailDto> OrderDetails { get; set; } = new();
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}

public class ServiceTypeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal BasePrice { get; set; }
}

public class StatusDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class DiagnosticDetailDto
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public string? Observation { get; set; }
    public decimal EstimatedCost { get; set; }
    public int Priority { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
}

public class OrderDetailDto
{
    public int Id { get; set; }
    public SpareDto Spare { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal? Discount { get; set; }
    public decimal Subtotal { get; set; }
}

