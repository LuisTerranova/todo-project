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

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var todo = await _context.Todos.FindAsync(id);  
        if (todo == null)  
        {  
            return NotFound();  
        }  
  
        var viewModel = new TodoViewModel  
        {  
            Title = todo.Title,  
            Description = todo.Body  
        };
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
                TempData["Info"] = "No task found.";
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
            TempData["Info"] = "Something went wrong.";
            return RedirectToAction("ShowAll");
        }
    }

    [HttpGet]
    public async Task<IActionResult> ShowAll()
    {
        try
        {
            var todo = await context.Todos.ToListAsync();
            
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

    [HttpDelete]
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

    [HttpPost]
    public async Task<IActionResult> ToggleCompletion(int id)
    {
            var todo = await context.Todos.FindAsync(id);

            todo.IsCompleted = !todo.IsCompleted;
            await context.SaveChangesAsync();
            return RedirectToAction("ShowAll");
    }
}