using Microsoft.AspNetCore.Identity;

namespace Todo.Api.Models;

public class User : IdentityUser<long> /*Creation of user class inheriting the
identityuser from ASPNET IDENTITY package, for easier user handling(auth, login, etc)*/
{
    //RegisterDate attribute to track user register date
    public DateTime RegisterDate { get; set; } = DateTime.Now; //default value
}