using System.Security.Claims;
using Todo.Api.Common.Api;
using Todo.Core.Handlers;
using Todo.Core.Requests.Chores;

namespace Todo.Api.Endpoints.ChoreEndpoints;

public class GetChoreByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync)
            .RequireAuthorization()
            .WithName("Chores : Get By Id")
            .WithSummary("Returns a chore")
            .WithOrder(2);

    private static async Task<IResult> HandleAsync(ClaimsPrincipal user, IChoreHandler handler, long id)
    {
        var request = new GetChoreByIdRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id
        };
        
        var result = await handler.GetByIdAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result) 
            : TypedResults.NotFound(result);
    }
}