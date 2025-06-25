using Application.Common.DTOs.Spares;

namespace Application.Common.DTOs.ServiceOrders;

/// <summary>
/// DTO que representa un detalle de repuestos en una orden de servicio.
/// Contiene la información de los repuestos utilizados y sus cantidades.
/// </summary>
public class OrderDetailDto
{
    /// <summary>
    /// Identificador único del detalle de orden
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Información completa del repuesto utilizado
    /// </summary>
    public SpareDto Spare { get; set; } = null!;

    /// <summary>
    /// Cantidad de unidades del repuesto
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Precio unitario del repuesto al momento de la venta
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Descuento aplicado al repuesto (si aplica)
    /// </summary>
    public decimal? Discount { get; set; }

    /// <summary>
    /// Subtotal del detalle (Cantidad * Precio Unitario - Descuento)
    /// </summary>
    public decimal Subtotal { get; set; }
} 