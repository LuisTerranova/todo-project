using Todo.Api.Common.Api;
using Todo.Api.Endpoints.ChoreEndpoints;
using Todo.Api.Models;

namespace Todo.Api.Endpoints;
//Endpoint class to map endpoints outside of program
public static class Endpoint
{
    //Mapping method
    public static void MapEndpoints(this WebApplication app)
    {
        //Creation of base group
        var endpoints = app.MapGroup("");
        //Mapping CRUD endpoints
        endpoints.MapGroup("v1/chores")
            .WithTags("Chores")
            .MapEndpoint<CreateChoreEndpoint>()
            .MapEndpoint<GetChoreByIdEndpoint>()
            .MapEndpoint<UpdateChoreEndpoint>()
            .MapEndpoint<GetAllChoresEndpoint>()
            .MapEndpoint<DeleteChoreEndpoint>();
        /*Mapping Identity endpoint. Specifically with MapIdentityApi for
         standardized endpoints regarding User related actions*/
        endpoints.MapGroup("v1/identity").MapIdentityApi<User>();
    }
    
    /*Simplifies API endpoint registration, calling the map method created on the
     CRUD endpoints, used mainly to chain methods and make the code more readable.*/
    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
        where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
}