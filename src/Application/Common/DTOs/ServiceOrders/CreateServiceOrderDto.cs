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
    /// ID del estado de la orden
    /// </summary>
    public int StatusId { get; set; }

    /// <summary>
    /// Fecha de entrada de la orden
    /// </summary>
    public DateTime EntryDate { get; set; }

    /// <summary>
    /// Fecha de salida de la orden
    /// </summary>
    public DateTime? ExitDate { get; set; }

    /// <summary>
    /// Mensaje o comentarios del cliente sobre el servicio requerido
    /// </summary>
    public string? CustomerMessage { get; set; }

    /// <summary>
    /// Lista de detalles de diagnóstico en la orden
    /// </summary>
    public List<CreateDiagnosticDetailDto> DiagnosticDetails { get; set; } = new();

    /// <summary>
    /// Lista de detalles de repuestos en la orden
    /// </summary>
    public List<CreateOrderDetailDto> OrderDetails { get; set; } = new();
}

/// <summary>
/// DTO para la creación de detalles de diagnóstico en una orden de servicio.
/// </summary>
public class CreateDiagnosticDetailDto
{
    /// <summary>
    /// Descripción del problema o condición encontrada
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Observaciones adicionales sobre el diagnóstico
    /// </summary>
    public string? Observation { get; set; }

    /// <summary>
    /// Costo estimado de la reparación
    /// </summary>
    public decimal EstimatedCost { get; set; }

    /// <summary>
    /// Nivel de prioridad del problema (1: Alta, 2: Media, 3: Baja)
    /// </summary>
    public int Priority { get; set; }
}

/// <summary>
/// DTO para la creación de detalles de repuestos en una orden de servicio.
/// </summary>
public class CreateOrderDetailDto
{
    /// <summary>
    /// ID del repuesto a utilizar
    /// </summary>
    public int SpareId { get; set; }

    /// <summary>
    /// Cantidad de unidades del repuesto
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Precio unitario del repuesto
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Descuento a aplicar (si aplica)
    /// </summary>
    public decimal? Discount { get; set; }
} 