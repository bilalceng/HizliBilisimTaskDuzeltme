using System.ComponentModel.DataAnnotations;

public class InvoiceCreateDto
{
    [Required(ErrorMessage = "CustomerId is required")]
    public int CustomerId { get; set; }

    [Required(ErrorMessage = "InvoiceNumber is required")]
    [StringLength(50)]
    public string InvoiceNumber { get; set; } = string.Empty;

    [Required]
    public DateTime InvoiceDate { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public decimal TotalAmount { get; set; }

    public DateTime RecordDate { get; set; }

    public int UserId { get; set; } 
}