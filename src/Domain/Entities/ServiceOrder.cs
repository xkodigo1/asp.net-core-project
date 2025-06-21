using AutoTallerManager.Domain.Entities;

namespace Domain.Entities;

public class ServiceOrder : BaseEntity
{
    public int VehicleId { get; set; }
    public int MechanicId { get; set; }
    public int ServiceTypeId { get; set; }
    public int StatusId { get; set; }
    public DateTime EntryDate { get; set; }
    public DateTime? ExitDate { get; set; }
    public string? CustomerMessage { get; set; }

    // Navigation properties
    public Vehicle Vehicle { get; set; } = null!;
    public User Mechanic { get; set; } = null!;
    public ServiceType ServiceType { get; set; } = null!;
    public Status Status { get; set; } = null!;
    public ICollection<DiagnosticDetail> DiagnosticDetails { get; private set; } = new List<DiagnosticDetail>();
    public ICollection<OrderDetail> OrderDetails { get; private set; } = new List<OrderDetail>();
    public ICollection<InventoryDetail> InventoryDetails { get; private set; } = new List<InventoryDetail>();
    public Invoice? Invoice { get; set; }
} 