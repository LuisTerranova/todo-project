using System.ComponentModel.DataAnnotations;

namespace Todo.ViewModels.Accounts;

public class RegisterViewModel(string name, string email, string password)
{
    [Required(ErrorMessage = "Username is required")]
    [StringLength(80, ErrorMessage = "Username cannot be longer than 80 characters.", MinimumLength = 2)]
    public string Name { get; set; } = name;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email")]
    public string Email { get; set; } = email;

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    [StringLength(25, ErrorMessage = "The Password must be between {2} and {1} characters long.", MinimumLength = 8)]
    public string Password { get; set; } = password;
}