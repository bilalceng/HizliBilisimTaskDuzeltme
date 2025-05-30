namespace HizliBilisim.DTOs;

using System.ComponentModel.DataAnnotations;

public class CustomerDto
{
    public int CustomerId { get; set; }
    
    [Required(ErrorMessage = "Tax number is required.")]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "Tax number must be between 5 and 20 characters.")]
    public string TaxNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Title is required.")]
    [StringLength(100, ErrorMessage = "Title must be under 100 characters.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Address is required.")]
    [StringLength(200, ErrorMessage = "Address must be under 200 characters.")]
    public string Address { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string EMail { get; set; } = string.Empty;

    [Required(ErrorMessage = "UserId is required.")]
    public int UserId { get; set; }
}
