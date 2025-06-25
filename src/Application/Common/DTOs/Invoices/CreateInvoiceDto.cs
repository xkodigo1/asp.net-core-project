namespace Application.Common.DTOs.Invoices;

public class CreateInvoiceDto
{
    public int ServiceOrderId { get; set; }
    public decimal TaxRate { get; set; }
    public decimal DiscountRate { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public string? Notes { get; set; }
} 