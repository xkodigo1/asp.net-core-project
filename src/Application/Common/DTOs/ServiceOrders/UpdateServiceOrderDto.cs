namespace Application.Common.DTOs.ServiceOrders;

/// <summary>
/// DTO para la actualización de órdenes de servicio existentes.
/// Permite modificar los detalles y asignaciones de una orden de servicio.
/// </summary>
public class UpdateServiceOrderDto
{
    /// <summary>
    /// ID de la orden de servicio
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ID del vehículo asociado a la orden
    /// </summary>
    public int VehicleId { get; set; }

    /// <summary>
    /// ID del mecánico asignado a la orden
    /// </summary>
    public int MechanicId { get; set; }

    /// <summary>
    /// ID del tipo de servicio
    /// </summary>
    public int ServiceTypeId { get; set; }

    /// <summary>
    /// ID del estado de la orden
    /// </summary>
    public int StatusId { get; set; }

    /// <summary>
    /// Fecha y hora de entrada del vehículo
    /// </summary>
    public DateTime EntryDate { get; set; }

    /// <summary>
    /// Fecha y hora de salida del vehículo
    /// </summary>
    public DateTime? ExitDate { get; set; }

    /// <summary>
    /// Mensaje o comentarios del cliente
    /// </summary>
    public string? CustomerMessage { get; set; }

    /// <summary>
    /// Lista actualizada de detalles de diagnóstico
    /// </summary>
    public List<CreateDiagnosticDetailDto> DiagnosticDetails { get; set; } = new();

    /// <summary>
    /// Lista actualizada de repuestos utilizados
    /// </summary>
    public List<CreateOrderDetailDto> OrderDetails { get; set; } = new();
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