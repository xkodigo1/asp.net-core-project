namespace Application.Common.DTOs.ServiceTypes;

public class CreateServiceTypeDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int EstimatedTime { get; set; }
    public decimal BasePrice { get; set; }
} 