namespace Domain.Entities;

/// <summary>
/// Representa una orden de servicio en el taller.
/// Contiene toda la información relacionada con un servicio prestado a un vehículo,
/// incluyendo diagnósticos, repuestos utilizados y facturación.
/// </summary>
public class ServiceOrder : BaseEntity
{
    /// <summary>
    /// ID del vehículo al que se le realiza el servicio
    /// </summary>
    public int VehicleId { get; set; }

    /// <summary>
    /// ID del mecánico asignado a la orden
    /// </summary>
    public int MechanicId { get; set; }

    /// <summary>
    /// ID del tipo de servicio a realizar
    /// </summary>
    public int ServiceTypeId { get; set; }

    /// <summary>
    /// ID del estado actual de la orden
    /// </summary>
    public int StatusId { get; set; }

    /// <summary>
    /// Fecha y hora de ingreso del vehículo al taller
    /// </summary>
    public DateTime EntryDate { get; set; }

    /// <summary>
    /// Fecha y hora de salida del vehículo del taller (puede ser null si aún no ha salido)
    /// </summary>
    public DateTime? ExitDate { get; set; }

    /// <summary>
    /// Mensaje o comentarios del cliente sobre el servicio requerido
    /// </summary>
    public string? CustomerMessage { get; set; }

    // Navigation properties
    /// <summary>
    /// Vehículo al que se le realiza el servicio
    /// </summary>
    public Vehicle Vehicle { get; set; } = null!;
    /// <summary>
    /// Mecánico asignado a la orden
    /// </summary>
    public User Mechanic { get; set; } = null!;
    /// <summary>
    /// Tipo de servicio a realizar
    /// </summary>
    public ServiceType ServiceType { get; set; } = null!;
    /// <summary>
    /// Estado actual de la orden
    /// </summary>
    public Status Status { get; set; } = null!;
    /// <summary>
    /// Lista de diagnósticos realizados durante el servicio
    /// </summary>
    public ICollection<DiagnosticDetail> DiagnosticDetails { get; private set; } = new List<DiagnosticDetail>();
    /// <summary>
    /// Lista de repuestos utilizados en el servicio
    /// </summary>
    public ICollection<OrderDetail> OrderDetails { get; private set; } = new List<OrderDetail>();
    /// <summary>
    /// Factura asociada a la orden (puede ser null si aún no se ha facturado)
    /// </summary>
    public Invoice? Invoice { get; set; }
} 