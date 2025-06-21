namespace Domain.Entities;

public class Invoice : BaseEntity
{
    public Guid ServiceOrderId { get; set; }
    public DateTime IssueDate { get; set; }
    public decimal LaborTotal { get; set; }
    public decimal SparesTotal { get; set; }
    public decimal TotalAmount { get; set; }

    // Navigation properties
    public ServiceOrder ServiceOrder { get; set; } = null!;
} 