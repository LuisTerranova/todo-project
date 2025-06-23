using Microsoft.AspNetCore.Identity;

namespace Todo.Api.Models;

public class User : IdentityUser
{
    public List<IdentityRole<long>>? Roles { get; set; }
}