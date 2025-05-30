using System.ComponentModel.DataAnnotations;

namespace HizliBilisim.DTOs;

public class InvoiceLineDto
{
    public int InvoiceLineId { get; set; }

    [Required(ErrorMessage = "InvoiceId is required")]
    public int InvoiceId { get; set; }

    [Required(ErrorMessage = "ItemName is required")]
    [StringLength(100, ErrorMessage = "ItemName cannot be longer than 100 characters")]
    public string ItemName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Quantity is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
    public int Quantity { get; set; }

    [Required(ErrorMessage = "Price is required")]
    [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number")]
    public decimal Price { get; set; }

    public DateTime RecordDate { get; set; }

    [Required(ErrorMessage = "UserId is required")]
    public int UserId { get; set; }
}
