using HexaPOS.Application.Common.Interfaces;
using HexaPOS.Application.Common.Models;
using HexaPOS.Application.Features.Account.Auth;
using HexaPOS.Application.Features.Account.Register;
using HexaPOS.Web.Infraestructure.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HexaPOS.Web.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/auth")
            .WithTags("Auth");

        group.MapPost("register", Register);
        group.MapPost("/login", Auth);
    }


    public static async Task<IResult> Auth(AuthRequest request,IHandler<AuthRequest, Result<string>> handler, CancellationToken ct)
    {
        var response = await handler.HandleAsync(request, ct);

        return response.ToHttpResult();
    }

    public static async Task<IResult> Register(RegisterAccountRequest request, IHandler<RegisterAccountRequest, Result> handler, CancellationToken ct)
    {
        var response = await handler.HandleAsync(request, ct);

        return response.ToHttpResult();
    }
}