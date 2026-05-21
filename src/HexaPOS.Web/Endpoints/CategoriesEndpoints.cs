using HexaPOS.Application.Common.Interfaces;
using HexaPOS.Application.Common.Models;
using HexaPOS.Application.Features.Categories;
using HexaPOS.Application.Features.Products;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HexaPOS.Web.Endpoints;

public static class CategoriesEndpoints
{
    public static void MapCategoriesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/categories")
            .WithTags("Categories");

        group.MapPost("/sync/bulk-sync", BulkSync);
    }

    public static async Task<Results<Ok, BadRequest>> BulkSync(BulkSyncCategoriesRequest request, IHandler<BulkSyncCategoriesRequest, Result> handler, CancellationToken ct)
    {
        var response = await handler.HandleAsync(request, ct);

        if (!response.Success)
        {
            return TypedResults.BadRequest();
        }

        return TypedResults.Ok();
    }
}
