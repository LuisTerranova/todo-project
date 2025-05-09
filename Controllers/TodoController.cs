using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Models;
using Todo.ViewModels;

namespace Todo.Controllers;

public class TodoController : ControllerBase
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
            Body = model.Description,
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


}