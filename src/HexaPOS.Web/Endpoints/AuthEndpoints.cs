using HexaPOS.Application.Common.Interfaces;
using HexaPOS.Application.Features.Account.Auth;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HexaPOS.Web.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/auth")
            .WithTags("Auth");

        group.MapPost("/register", Register);
        group.MapPost("/login", Auth);
    }

    public static async Task<Ok<string>> Register()
    {
        return TypedResults.Ok("ok");
    }

    public static async Task<Ok<string>> Auth(AuthRequest request, IHandler<AuthRequest, string> handler, CancellationToken ct)
    {
        var response = await handler.HandleAsync(request, ct);
        
        return TypedResults.Ok(response);
    }
}