namespace Application.Common.DTOs.Invoices;

public class UpdateInvoiceDto
{
    public string? Notes { get; set; }
    public string PaymentStatus { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
} 