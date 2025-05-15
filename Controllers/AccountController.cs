using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;
using Todo.Data;
using Todo.Models;
using Todo.ViewModels;

namespace Todo.Controllers;

public class AccountController(TodoDataContext _context) : Controller
{
    private readonly TodoDataContext context = _context;

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        var user = new User
        {
            Name = model.Name,
            Email = model.Email,
            PasswordHash = PasswordHasher.Hash(password: model.Password)
        };

        try
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            TempData["Error"] = "Email already in use";
            return View("Register");
        }
        
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        
    }
    
}