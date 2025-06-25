namespace Application.Common.DTOs.ServiceOrders;

/// <summary>
/// DTO para la creación de nuevas órdenes de servicio.
/// Contiene solo los campos necesarios para crear una nueva orden,
/// excluyendo campos calculados o que se generan automáticamente.
/// </summary>
public class CreateServiceOrderDto
{
    /// <summary>
    /// ID del vehículo al que se le realizará el servicio
    /// </summary>
    public int VehicleId { get; set; }

    /// <summary>
    /// ID del mecánico que será asignado a la orden
    /// </summary>
    public int MechanicId { get; set; }

    /// <summary>
    /// ID del tipo de servicio a realizar
    /// </summary>
    public int ServiceTypeId { get; set; }

    /// <summary>
    /// Mensaje o comentarios del cliente sobre el servicio requerido
    /// </summary>
    public string? CustomerMessage { get; set; }

    /// <summary>
    /// Lista de IDs de repuestos que se utilizarán en el servicio
    /// </summary>
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