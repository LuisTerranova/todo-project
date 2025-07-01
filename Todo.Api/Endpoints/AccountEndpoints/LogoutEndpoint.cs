using Microsoft.AspNetCore.Identity;
using Todo.Api.Common.Api;
using Todo.Api.Models;

namespace Todo.Api.Endpoints.AccountEndpoints;

public class LogoutEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/logout", HandleAsync)
            .RequireAuthorization();

    private static async Task<IResult> HandleAsync(SignInManager<User> signInManager)
    {
        await signInManager.SignOutAsync();
        return Results.Ok();
    }
}