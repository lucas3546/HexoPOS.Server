using HexaPOS.Application.Common.Interfaces;
using HexaPOS.Application.Common.Models;
using HexaPOS.Application.Features.Products.Bulk;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HexaPOS.Web.Endpoints;

public static class ProductsEndpoints
{
    public static void MapProductsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/products")
            .WithTags("Products");

        group.MapPost("/sync/bulk-upsert", BulkUpsert);
    }
    
    public static async Task<Results<Ok, BadRequest>> BulkUpsert(BulkSyncProductsRequest request, IHandler<BulkSyncProductsRequest, Result> handler, CancellationToken ct)
    {
        var response = await handler.HandleAsync(request, ct);

        if (!response.Success)
        {
            return TypedResults.BadRequest();
        }
        
        return TypedResults.Ok();
    }
}