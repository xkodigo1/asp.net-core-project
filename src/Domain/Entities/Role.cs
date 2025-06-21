namespace Domain.Entities;

public class Role : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    
    // Navigation properties
    public ICollection<UserRole> UserRoles { get; private set; } = new List<UserRole>();
} 