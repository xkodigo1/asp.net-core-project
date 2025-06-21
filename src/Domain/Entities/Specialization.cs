namespace Domain.Entities;

public class Specialization : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    // Navigation properties
    public ICollection<UserSpecialization> UserSpecializations { get; private set; } = new List<UserSpecialization>();
} 