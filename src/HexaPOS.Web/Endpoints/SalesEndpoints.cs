using HexaPOS.Application.Common.Interfaces;
using HexaPOS.Application.Common.Models;
using HexaPOS.Application.Features.Products;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HexaPOS.Web.Endpoints;

public static class SalesEndpoints
{
    public static void MapSalesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/sales")
            .WithTags("Sales");

        group.MapPost("/sync/bulk-sync", BulkSync);
    }

    public static async Task<Results<Ok, BadRequest>> BulkSync(BulkSyncProductsRequest request, IHandler<BulkSyncProductsRequest, Result> handler, CancellationToken ct)
    {
        var response = await handler.HandleAsync(request, ct);

        if (!response.Success)
        {
            return TypedResults.BadRequest();
        }

        return TypedResults.Ok();
    }
}
