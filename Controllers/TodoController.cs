using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Models;
using Todo.ViewModels;

namespace Todo.Controllers;

[Authorize]
public class TodoController(TodoDataContext context) : Controller
{
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(TodoViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);
        
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        try
        {
            if (int.TryParse(userIdString, out var userId))
            {
                var todo = new TodoModel(model.Title, model.Description, userId);
                await context.Todos.AddAsync(todo);
                await context.SaveChangesAsync();
            }
            TempData["SuccessMessage"] = "Your task was successfully created.";
            return RedirectToAction("Index","UserArea");
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var todo = await context.Todos.FindAsync(id);  
        
        if (todo == null)  
        {  
            return NotFound();  
        }

        var viewModel = new TodoViewModel(todo.Title, todo.Body);
        
        return View(viewModel);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(int id, TodoViewModel model )
    {
        try
        {
            var todo = await context.Todos.FindAsync(id);

            if (todo == null)
            {
                TempData["Info"] = "No tasks found.";
                return RedirectToAction("ShowAll");
            }
            
            todo.Title = model.Title;
            todo.Body = model.Description;

            context.Update(todo);
            await context.SaveChangesAsync();
            
            TempData["SuccessMessage"] = "Your task was successfully updated.";
            return RedirectToAction("ShowAll");
        }
        catch
        {
            TempData["Info"] = "Something went wrong. Try again later.";
            return RedirectToAction("ShowAll");
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> ShowAll()
    {
        try
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) 
                                   ?? throw new InvalidOperationException("User may not be properly authenticated."));
            var todo = await context.Todos.Where(x => x.UserId == userId)
                .ToListAsync();

            if (todo.Count != 0) 
                return View("ShowAll", todo);
            
            TempData["Info"] = "No tasks found, create your first task.";
            return View("Create");
        }
        catch(Exception)
        {
            TempData["Error"] = "An error occurred while retrieving tasks.";
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            {
                TempData["Error"] = "Invalid user ID.";  
                return RedirectToAction("ShowAll");
            }
            
            var todo = await context.Todos.Where(x=>x.UserId == userId)
                .FirstOrDefaultAsync();
            
            if (todo == null)
            {
                TempData["Info"] = "No task found";
                return RedirectToAction("ShowAll");
            }
            
            context.Todos.Remove(todo);
            await context.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            TempData["Error"] = "An error occurred while processing tasks.";
            return RedirectToAction("ShowAll");
        }

        TempData["SuccessMessage"] = "Your task was deleted.";
        return RedirectToAction("ShowAll");
    }

    [HttpPost]
    public async Task<IActionResult> ToggleCompletion(int id)
    {
            var todo = await context.Todos.FindAsync(id);

            if (todo != null) todo.IsCompleted = !todo.IsCompleted;
            await context.SaveChangesAsync();
            return RedirectToAction("ShowAll");
    }
}