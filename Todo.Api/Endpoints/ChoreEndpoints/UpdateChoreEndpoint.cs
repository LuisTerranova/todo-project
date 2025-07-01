using System.Security.Claims;
using Todo.Api.Common.Api;
using Todo.Core.Handlers;
using Todo.Core.Requests.Chores;

namespace Todo.Api.Endpoints.ChoreEndpoints;

public class UpdateChoreEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
            .RequireAuthorization()
            .WithName("Chores : Update")
            .WithSummary("Updates a existing chore")
            .WithOrder(3);

    private static async Task<IResult> HandleAsync(ClaimsPrincipal user, IChoreHandler handler, UpdateChoreRequest request, long id)
    {
        request.UserId = user.Identity?.Name ?? string.Empty;
        request.Id = id;
            
        var result = await handler.UpdateAsync(request);
        return result.IsSuccess 
            ? TypedResults.Ok(result) //posso utilizar o typed results para nao ter que-> prox 
            : TypedResults.BadRequest(result); //utilizar o produces la em cima apos o handleasync.
    }
}