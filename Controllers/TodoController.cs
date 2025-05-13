using System.Transactions;
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

        var todo = new TodoModel(model.Title, model.Description);
        

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

    public async Task<IActionResult> Edit(int id, TodoViewModel model )
    {
        try
        {
            var todo = await context.Todos.FindAsync(id);

            if (todo == null)
            {
                TempData["Info"] = "No task found.";
                return RedirectToAction("ShowAll");
            }


            todo.Title = model.Title;
            todo.Body = model.Description;

            context.Update(todo);
            await context.SaveChangesAsync();
            
            TempData["SuccessMessage"] = "Your task was succesfully updated.";
            return RedirectToAction("ShowAll");
        }
        catch
        {
            TempData["Info"] = "Something went wrong.";
            return RedirectToAction("ShowAll");
        }
    }

    [HttpGet]
    public async Task<IActionResult> ShowAll(TodoModel model)
    {
        try
        {
            var todo = await context.Todos.AsNoTracking()
                                                            .ToListAsync();
            
            if (todo.Count == 0) 
                TempData["Info"] = "No tasks found";
                
            return View(todo);
        }
        catch(Exception ex)
        {
            TempData["Error"] = "An error occurred while retrieving tasks.";
            return StatusCode(500, "Internal server error");
        }
    }

    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var todo = await context.Todos.FindAsync(id);

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
}