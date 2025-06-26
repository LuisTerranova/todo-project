using Todo.Api.Common.Api;
using Todo.Api.Endpoints.ChoreEndpoints;

namespace Todo.Api.Endpoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app.MapGroup("");

        endpoints.MapGroup("v1/chores")
            .WithTags("Chores")
            .MapEndpoint<CreateChoreEndpoint>();

    }
    
    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
        where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
}