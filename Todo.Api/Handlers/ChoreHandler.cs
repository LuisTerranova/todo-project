using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Api.Data;
using Todo.Core.Handlers;
using Todo.Core.Models;
using Todo.Core.Requests.Chores;
using Todo.Core.Responses;

namespace Todo.Api.Handlers;

public class ChoreHandler(AppDbContext context) : IChoreHandler
{
    public async Task<Response<Chore?>> CreateAsync(CreateChoreRequest request)
    {
        try
        {
            var chore = new Chore
            {
                UserId = request.UserId,
                Title = request.Title,
                Description = request.Description,
                DueDate = request.DueDate
            };

            await context.Chores.AddAsync(chore);
            await context.SaveChangesAsync();
            return new Response<Chore?>(chore, 201, "Chore Created successfully");
        }
        catch
        {
            return new Response<Chore?>(null, 500, "Internal Server Error");
        }
    }

    public async Task<Response<Chore?>> GetByIdAsync(GetChoreByIdRequest request)
    {
        try
        {
            var chore = await context.Chores
                .FirstOrDefaultAsync(x => x.Id == request.Id
                                          && x.UserId == request.UserId);
            return chore == null 
                ? new Response<Chore?>(null, 404, "Chore could not be found") 
                : new Response<Chore?>(chore, 200, "Chore found successfully");
        }
        catch
        {
            return new Response<Chore?>(null, 500, "Internal Server Error");
        }
    }

    public async Task<Response<Chore?>> UpdateAsync(UpdateChoreRequest request)
    {
        try
        {
            var chore = await context.Chores.FirstOrDefaultAsync(x => x.Id == request.Id
                                                                      && x.UserId == request.UserId);
            if (chore == null)
                return new Response<Chore?>(null, 404, "Chore could not be found");

            chore.Title = request.Title;
            chore.Description = request.Description;
            chore.DueDate = request.DueDate;

            context.Chores.Update(chore);
            await context.SaveChangesAsync();

            return new Response<Chore?>(chore, 200, "Chore updated successfully");
        }
        catch
        {
            return new Response<Chore?>(null, 500, "Internal Server Error");
        }
    }

    public async Task<Response<Chore?>> DeleteAsync(DeleteChoreRequest request)
    {
        try
        {
            var chore = context.Chores.FirstOrDefault(x => x.Id == request.Id
                                                           && x.UserId == request.UserId);

            if (chore == null)
                return new Response<Chore?>(null, 404, "Chore could not be found");

            context.Chores.Remove(chore);
            await context.SaveChangesAsync();

            return new Response<Chore?>(chore, 200, "Chore deleted successfully");
        }
        catch
        {
            return new Response<Chore?>(null, 500, "Internal Server Error");
        }
    }

    public async Task<PagedResponse<List<Chore>?>> GetAllAsync(GetAllChoresRequest request)
    {
        try
        {
            var query = context.Chores
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId);

            var chores = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();

            return new PagedResponse<List<Chore>?>(chores, count, request.PageNumber, request.PageSize);
        }
        catch 
        {
            return new PagedResponse<List<Chore>?>(null, 0, request.PageNumber, request.PageSize);
        }
    }
}