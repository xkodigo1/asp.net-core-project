namespace Application.Common.DTOs.Users;

public class CreateUserDto
{
    public string Username { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public List<int> RoleIds { get; set; } = new();
    public List<int> SpecializationIds { get; set; } = new();
} 