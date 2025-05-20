using System.ComponentModel.DataAnnotations;

namespace Todo.ViewModels.Accounts;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Username is required")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    [StringLength(25, ErrorMessage = "The Password must be between {2} and {1} characters long.", MinimumLength = 8)]
    public string Password { get; set; }
}