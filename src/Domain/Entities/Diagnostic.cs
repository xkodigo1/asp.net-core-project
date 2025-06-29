namespace Domain.Entities;

public class Diagnostic : BaseEntity
{
    public int ServiceOrderId { get; set; }
    public int UserId { get; set; }
    public string Description { get; set; } = string.Empty;
    public string? Recommendation { get; set; }
    public decimal EstimatedCost { get; set; }

    // Navigation properties
    public ServiceOrder ServiceOrder { get; set; } = null!;
    public User User { get; set; } = null!;
    public ICollection<DiagnosticDetail> DiagnosticDetails { get; private set; } = new List<DiagnosticDetail>();
} 