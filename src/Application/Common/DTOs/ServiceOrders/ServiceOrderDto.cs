using Application.Common.DTOs.Customers;
using Application.Common.DTOs.Inventory;
using Application.Common.DTOs.Invoices;
using Application.Common.DTOs.Spares;
using Application.Common.DTOs.Users;
using Application.Common.DTOs.Vehicles;

namespace Application.Common.DTOs.ServiceOrders;

/// <summary>
/// DTO (Data Transfer Object) que representa una orden de servicio.
/// Se utiliza para transferir información de órdenes de servicio entre la API y los clientes,
/// incluyendo información relacionada como vehículo, mecánico, diagnósticos y repuestos.
/// </summary>
public class ServiceOrderDto
{
    /// <summary>
    /// Identificador único de la orden de servicio
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Información completa del vehículo asociado a la orden
    /// </summary>
    public VehicleDto Vehicle { get; set; } = null!;

    /// <summary>
    /// Información del mecánico asignado a la orden
    /// </summary>
    public UserDto Mechanic { get; set; } = null!;

    /// <summary>
    /// Tipo de servicio a realizar (ej: mantenimiento preventivo, reparación, etc.)
    /// </summary>
    public ServiceTypeDto ServiceType { get; set; } = null!;

    /// <summary>
    /// Estado actual de la orden (ej: en espera, en proceso, finalizada, etc.)
    /// </summary>
    public StatusDto Status { get; set; } = null!;

    /// <summary>
    /// Fecha y hora de ingreso del vehículo al taller
    /// </summary>
    public DateTime EntryDate { get; set; }

    /// <summary>
    /// Fecha y hora de salida del vehículo del taller (null si aún no ha salido)
    /// </summary>
    public DateTime? ExitDate { get; set; }

    /// <summary>
    /// Mensaje o comentarios del cliente sobre el servicio requerido
    /// </summary>
    public string? CustomerMessage { get; set; }

    /// <summary>
    /// Lista de diagnósticos realizados durante el servicio
    /// </summary>
    public ICollection<DiagnosticDetailDto> DiagnosticDetails { get; set; } = new List<DiagnosticDetailDto>();

    /// <summary>
    /// Lista de repuestos utilizados en el servicio
    /// </summary>
    public ICollection<OrderDetailDto> OrderDetails { get; set; } = new List<OrderDetailDto>();

    /// <summary>
    /// Lista de movimientos de inventario relacionados con la orden
    /// </summary>
    public ICollection<InventoryDetailDto> InventoryDetails { get; set; } = new List<InventoryDetailDto>();

    /// <summary>
    /// Información de la factura asociada (si la orden ya fue facturada)
    /// </summary>
    public InvoiceDto? Invoice { get; set; }

    /// <summary>
    /// Monto total de la orden, incluyendo repuestos y mano de obra
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Fecha y hora de creación del registro
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Usuario que creó el registro
    /// </summary>
    public string CreatedBy { get; set; } = string.Empty;

    /// <summary>
    /// Fecha y hora de la última actualización
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Usuario que realizó la última actualización
    /// </summary>
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

