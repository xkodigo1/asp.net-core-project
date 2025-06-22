using Application.Common.DTOs.Vehicles;

namespace Application.Common.DTOs.Customers;

public class CustomerDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string? TaxId { get; set; }
    public string? CompanyName { get; set; }
    public string Identification { get; set; } = string.Empty;
    public List<VehicleDto> Vehicles { get; set; } = new();
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
} 