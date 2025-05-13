using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Models;
using Todo.ViewModels;

namespace Todo.Controllers;

public class TodoController(TodoDataContext _context) : Controller
{
    private readonly TodoDataContext context = _context;

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        TodoViewModel model)
    {
        if (!ModelState.IsValid)
            return await Task.FromResult(BadRequest());

        var todo = new TodoModel
        {
            Title = model.Title,
            Body = model.Description
        };

        try
        {
            await context.Todos.AddAsync(todo);
            await context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Your task was succesfully created.";
            return RedirectToAction("Create");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> ShowAll(TodoModel model)
    {
        try
        {
            var todo = await context.Todos.AsNoTracking().ToListAsync();
            
            if (todo.Count != 0)
                return View(todo);
                
            return (IActionResult)(TempData["Error"] = "Your tasks are empty.");
        }
        catch
        {
            return StatusCode(500, "Internal server error");
        }
    }

    public async Task<IActionResult> Delete(int id)
    {
        var todo = await context.Todos.FirstOrDefaultAsync(x => x.Id == id);

        if (todo != null)
        {
            context.Todos.Remove(todo);
            await context.SaveChangesAsync();
        }

        TempData["SuccessMessage"] = "Your task was deleted.";
        return RedirectToAction("ShowAll");
    }
}