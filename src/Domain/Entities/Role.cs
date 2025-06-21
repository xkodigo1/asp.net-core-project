namespace Domain.Entities;

public class Role : BaseEntity
{
    public string Description { get; set; } = string.Empty;
    
    // Navigation properties
    public ICollection<UserRole> UserRoles { get; private set; } = new List<UserRole>();
} 