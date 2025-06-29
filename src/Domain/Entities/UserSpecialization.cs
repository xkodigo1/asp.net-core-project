namespace Domain.Entities;

public class UserSpecialization : BaseEntity
{
    public int UserId { get; set; }
    public int SpecializationId { get; set; }

    // Navigation properties
    public User User { get; set; } = null!;
    public Specialization Specialization { get; set; } = null!;
} 