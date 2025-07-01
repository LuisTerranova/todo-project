using Todo.Api.Common.Api;
using Todo.Core;
using Todo.Core.Handlers;
using Todo.Core.Requests.Chores;

namespace Todo.Api.Endpoints.ChoreEndpoints;

public class GetAllChoresEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .RequireAuthorization()
            .WithName("Chores : Get All Chores")
            .WithSummary("Returns a list of all chores")
            .WithOrder(4);

    private static async Task<IResult> HandleAsync(IChoreHandler handler,
        int pageNumber = Configuration.DefaultPageNumber,
        int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllChoresRequest
        {
            UserId = "teste@terra.io",
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        var result = await handler.GetAllAsync(request);
        
        return result.IsSuccess 
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}