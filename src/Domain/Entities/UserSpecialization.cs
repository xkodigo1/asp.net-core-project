namespace Domain.Entities;

public class UserSpecialization : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid SpecializationId { get; set; }

    // Navigation properties
    public User User { get; set; } = null!;
    public Specialization Specialization { get; set; } = null!;
} 