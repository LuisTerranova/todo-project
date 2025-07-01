using Todo.Api.Common.Api;
using Todo.Core.Handlers;
using Todo.Core.Requests.Chores;

namespace Todo.Api.Endpoints.ChoreEndpoints;

public class CreateChoreEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .RequireAuthorization()
            .WithName("Chores : Create")
            .WithSummary("Creates a new chore")
            .WithOrder(1);

    private static async Task<IResult> HandleAsync(IChoreHandler handler, CreateChoreRequest request)
    {
        request.UserId = "teste@terra.io";
        
        var result = await handler.CreateAsync(request);
        return result.IsSuccess 
            ? TypedResults.Created($"/{result.Data.Id}", result) //posso utilizar o typed results para nao ter que-> prox 
            : TypedResults.BadRequest(result); //utilizar o produces la em cima apos o handleasync.
    }
    
}