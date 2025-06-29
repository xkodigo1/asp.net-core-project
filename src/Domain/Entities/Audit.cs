namespace Domain.Entities;

public class Audit : BaseEntity
{
    public string AffectedEntity { get; set; } = string.Empty;
    public string ActionType { get; set; } = string.Empty;
    public int ResponsibleUserId { get; set; }
    public DateTime Timestamp { get; set; }

    // Navigation properties
    public User ResponsibleUser { get; set; } = null!;
} 