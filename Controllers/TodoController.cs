using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Models;
using Todo.ViewModels;

namespace Todo.Controllers;

public class TodoController : Controller
{
    [HttpPost("v1/todos")]
    public async Task<IActionResult> Post(
        [FromBody] TodoViewModel model,
        [FromServices] TodoDataContext context)
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

            return Ok();
        }
        catch (DbUpdateException)
        {
            return StatusCode(400);
        }
    }

    [HttpGet("v1/todos/{id}")]
    public async Task<IActionResult> Get(
        [FromServices] TodoDataContext context,
        [FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return await Task.FromResult(BadRequest());

        try
        {
            var todo = await context.Todos.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return Ok(todo);
        }
        catch
        {
            return NotFound();
        }
    }


}