using System.ComponentModel.DataAnnotations;

public class UserForCreationDto
{
    [Required(ErrorMessage = "UserName is required")]
    [StringLength(50, ErrorMessage = "UserName cannot be longer than 50 characters")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters")]
    public string Password { get; set; } = string.Empty;
}