using System.ComponentModel.DataAnnotations;

public class UserDto
{
    public int UserId { get; set; }

    [Required(ErrorMessage = "UserName is required")]
    [StringLength(50, ErrorMessage = "UserName cannot be longer than 50 characters")]
    public string UserName { get; set; } = string.Empty;

    public DateTime RecordDate { get; set; }
}
