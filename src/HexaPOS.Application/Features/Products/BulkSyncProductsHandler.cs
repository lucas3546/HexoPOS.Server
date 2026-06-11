using HexaPOS.Application.Common.Interfaces;
using HexaPOS.Application.Common.Models;
using HexaPOS.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace HexaPOS.Application.Features.Products;

public sealed class BulkSyncProductsRequest
{
    public List<ProductDto> Products { get; set; } = new();
}


public class BulkSyncProductsHandler : IHandler<BulkSyncProductsRequest, Result>
{
    private readonly ISyncService _syncService;

    public BulkSyncProductsHandler(ISyncService syncService)
    {
        _syncService = syncService;
    }

    public async Task<Result> HandleAsync(BulkSyncProductsRequest request, CancellationToken ct)
    {
        await _syncService.SyncAsync<Product, ProductDto>(
            request.Products,
            (entity, dto) =>
            {
                entity.Id = dto.Id;
                entity.Name = dto.Name;
                entity.Ubication = dto.Ubication;
                entity.Sku = dto.Sku;
                entity.Barcode = dto.Barcode;
                entity.ImageFileName  = dto.ImageFileName;
                entity.Price = dto.Price;
                entity.Stock = dto.Stock;
                entity.StockLimit = dto.StockLimit;
                entity.UpdatedAt = dto.UpdatedAt;
                entity.DeletedAt = dto.DeletedAt;
                entity.CategoryId = dto.CategoryId;
            },
            ct);

        return Result.Ok();
    }
}