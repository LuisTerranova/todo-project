using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
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

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);
        
        var user = new User
        {
            Name = model.Name,
            Email = model.Email,
            PasswordHash = PasswordHasher.Hash(model.Password)
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

        TempData["SuccessMessage"] = "Account successfully created";
        return RedirectToAction(nameof(HomeController.Index), "Home");
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        try
        {
            var user = await context.Users.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == model.Email);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password");
                return View(model);
            }
            
            if (!PasswordHasher.Verify(user.PasswordHash, model.Password))
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password");
                return View(model);
            }

            var claims = new List<Claim>
            {
                new (ClaimTypes.NameIdentifier, user.Id.ToString()),
                new (ClaimTypes.Email, user.Email),
                new(ClaimTypes.Name, user.Name)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return RedirectToAction("Index","UserArea");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
            return View(model);
        }
        
    }
    
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        HttpContext.Session.Clear();  
        
        Response.Cookies.Delete(".AspNetCore.Cookies");  
        
        TempData["SuccessMessage"] = "You have been successfully logged out.";  
        return RedirectToAction("Index", "Home");
    }
}
