namespace Domain.Entities;

public class Diagnostic : BaseEntity
{
    public Guid UserId { get; set; }
    public string Description { get; set; } = string.Empty;

    // Navigation properties
    public User User { get; set; } = null!;
    public ICollection<DiagnosticDetail> DiagnosticDetails { get; private set; } = new List<DiagnosticDetail>();
} 