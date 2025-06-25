namespace Application.Common.DTOs.ServiceOrders;

/// <summary>
/// DTO para actualizar órdenes de servicio existentes.
/// Permite modificar el estado, fechas y detalles de una orden en curso.
/// </summary>
public class UpdateServiceOrderDto
{
    /// <summary>
    /// ID de la orden de servicio a actualizar
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nuevo ID del mecánico asignado (opcional)
    /// </summary>
    public int? MechanicId { get; set; }

    /// <summary>
    /// Nuevo estado de la orden
    /// </summary>
    public int StatusId { get; set; }

    /// <summary>
    /// Nueva fecha de salida del vehículo (opcional)
    /// </summary>
    public DateTime? ExitDate { get; set; }

    /// <summary>
    /// Nuevos comentarios o actualización del mensaje del cliente
    /// </summary>
    public string? CustomerMessage { get; set; }

    /// <summary>
    /// Lista actualizada de diagnósticos realizados
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