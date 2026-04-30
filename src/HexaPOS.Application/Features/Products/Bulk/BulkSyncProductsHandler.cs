using HexaPOS.Application.Common.Interfaces;
using HexaPOS.Application.Common.Models;
using HexaPOS.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace HexaPOS.Application.Features.Products.Bulk;

public sealed class BulkSyncProductsRequest
{
    public List<ProductDto> Products { get; set; } = new();
}

public class ProductDto : BaseSyncModel
{
    public string Name { get; set; } = default!;
    public string Ubication { get; set; } = default!;
    public string Sku { get; set; } = default!;
    public string Barcode { get; set; } = default!;
    public string Attribute { get; set; } = default!;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int StockLimit { get; set; }
    public Guid CategoryId { get; set; }
}

public class BulkSyncProductsHandler : IHandler<BulkSyncProductsRequest, Result>
{
    private readonly IApplicationDbContext _context;

    public BulkSyncProductsHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> HandleAsync(BulkSyncProductsRequest request, CancellationToken ct)
    {
        if (request.Products.Count == 0)
            return Result.Ok();

        var ids = request.Products.Select(x => x.Id).ToList();

        var existing = await _context.Products
            .Where(x => ids.Contains(x.Id))
            .ToDictionaryAsync(x => x.Id, ct);

        foreach (var dto in request.Products)
        {
            if (!existing.TryGetValue(dto.Id, out var entity))
            {
                _context.Products.Add(new Product
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Ubication = dto.Ubication,
                    Sku = dto.Sku,
                    Barcode = dto.Barcode,
                    Attribute = dto.Attribute,
                    Price = dto.Price,
                    Stock = dto.Stock,
                    StockLimit = dto.StockLimit,
                    CategoryId = dto.CategoryId,
                });

                continue;
            }

            if (dto.DeletedAt != null)
            {
                if (entity.DeletedAt == null || dto.DeletedAt > entity.DeletedAt)
                {
                    entity.DeletedAt = dto.DeletedAt;
                    entity.UpdatedAt = dto.UpdatedAt;
                }

                continue;
            }

            if (dto.UpdatedAt > entity.UpdatedAt)
            {
                entity.Name = dto.Name;
                entity.Ubication = dto.Ubication;
                entity.Sku = dto.Sku;
                entity.Barcode = dto.Barcode;
                entity.Attribute = dto.Attribute;
                entity.Price = dto.Price;
                entity.Stock = dto.Stock;
                entity.StockLimit = dto.StockLimit;
                entity.CategoryId = dto.CategoryId;
            }
        }

        await _context.SaveChangesAsync(ct);

        return Result.Ok();
    }
}