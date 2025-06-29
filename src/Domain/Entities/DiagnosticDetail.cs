namespace Domain.Entities;

public class DiagnosticDetail : BaseEntity
{
    public int ServiceOrderId { get; set; }
    public int DiagnosticId { get; set; }
    public string Description { get; set; } = string.Empty;
    public string? Observation { get; set; }
    public decimal EstimatedCost { get; set; }
    public int Priority { get; set; }

    // Navigation properties
    public ServiceOrder ServiceOrder { get; set; } = null!;
    public Diagnostic Diagnostic { get; set; } = null!;
} 