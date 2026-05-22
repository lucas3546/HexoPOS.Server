using HexaPOS.Application.Common.Interfaces;
using HexaPOS.Application.Common.Models;
using HexaPOS.Application.Features.Products;
using HexaPOS.Application.Features.SaleItems;
using HexaPOS.Application.Features.Sales;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HexaPOS.Web.Endpoints;

public static class SalesItemsEndpoints
{
    public static void MapSaleItemsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/sales-items")
            .WithTags("SalesItems");

        group.MapPost("/sync/bulk-sync", BulkSync);
    }

    public static async Task<Results<Ok, BadRequest>> BulkSync(BulkSyncSaleItemsRequest
        request, IHandler<BulkSyncSaleItemsRequest, Result> handler, CancellationToken ct)
    {
        var response = await handler.HandleAsync(request, ct);

        if (!response.Success)
        {
            return TypedResults.BadRequest();
        }

        return TypedResults.Ok();
    }
}