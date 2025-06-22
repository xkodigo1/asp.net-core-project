namespace Domain.Entities;

public class Invoice : BaseEntity
{
    public Guid ServiceOrderId { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Tax { get; set; }
    public decimal Total { get; set; }
    public string? Notes { get; set; }

    // Navigation properties
    public ServiceOrder ServiceOrder { get; set; } = null!;
} 