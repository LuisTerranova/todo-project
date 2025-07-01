using System.Security.Claims;
using Todo.Api.Common.Api;
using Todo.Core.Handlers;
using Todo.Core.Requests.Chores;

namespace Todo.Api.Endpoints.ChoreEndpoints;

public class DeleteChoreEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandleAsync)
            .RequireAuthorization()
            .WithName("Chores : Delete Chore")
            .WithSummary("Deletes a chore")
            .WithOrder(5);

    private static async Task<IResult> HandleAsync(ClaimsPrincipal user, IChoreHandler handler, long id)
    {
        var request = new DeleteChoreRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id
        };
        var result = await handler.DeleteAsync(request);
        
        return result.IsSuccess 
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }

}