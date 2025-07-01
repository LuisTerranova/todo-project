using System.ComponentModel.DataAnnotations;

namespace Todo.Core.Requests.Account;

public class RegisterRequest : BaseRequest
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email")]
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "Invalid Password")]
    public string Password { get; set; } = string.Empty;
}