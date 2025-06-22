namespace Domain.Entities;

public class Diagnostic : BaseEntity
{
    public int UserId { get; set; }
    public string Description { get; set; } = string.Empty;
    public string? Recommendation { get; set; }
    public decimal EstimatedCost { get; set; }

    // Navigation properties
    public User User { get; set; } = null!;
    public ICollection<DiagnosticDetail> DiagnosticDetails { get; private set; } = new List<DiagnosticDetail>();
} 