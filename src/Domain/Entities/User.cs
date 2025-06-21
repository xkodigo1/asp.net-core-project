namespace Domain.Entities;

public class User : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;

    // Navigation properties
    public ICollection<UserRole> UserRoles { get; private set; } = new List<UserRole>();
    public ICollection<UserSpecialization> UserSpecializations { get; private set; } = new List<UserSpecialization>();
    public ICollection<ServiceOrder> ServiceOrders { get; private set; } = new List<ServiceOrder>();
    public ICollection<Diagnostic> Diagnostics { get; private set; } = new List<Diagnostic>();
    public ICollection<Audit> Audits { get; private set; } = new List<Audit>();
} 