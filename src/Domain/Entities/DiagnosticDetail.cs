namespace Domain.Entities;

public class DiagnosticDetail
{
    public Guid ServiceOrderId { get; set; }
    public Guid DiagnosticId { get; set; }

    // Navigation properties
    public ServiceOrder ServiceOrder { get; set; } = null!;
    public Diagnostic Diagnostic { get; set; } = null!;
} 