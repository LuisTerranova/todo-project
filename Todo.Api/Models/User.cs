using Microsoft.AspNetCore.Identity;

namespace Todo.Api.Models;

public class User : IdentityUser<long>
{
    public DateTime RegisterDate { get; set; }
}