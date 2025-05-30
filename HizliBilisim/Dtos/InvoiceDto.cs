using System.ComponentModel.DataAnnotations;

public class InvoiceDto
{
    
    public int InvoiceId { get; set; }
    
    [Required(ErrorMessage = "CustomerId is required")]
    public int CustomerId { get; set; }

    [Required(ErrorMessage = "InvoiceNumber is required")]
    [StringLength(50, ErrorMessage = "InvoiceNumber cannot be longer than 50 characters")]
    public string InvoiceNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "InvoiceDate is required")]
    public DateTime InvoiceDate { get; set; }

    [Required(ErrorMessage = "TotalAmount is required")]
    [Range(0, double.MaxValue, ErrorMessage = "TotalAmount must be a positive number")]
    public decimal TotalAmount { get; set; }

    public DateTime RecordDate { get; set; }

    [Required(ErrorMessage = "UserId is required")]
    public int UserId { get; set; }
}