using HexaPOS.Application.Common.Interfaces;
using HexaPOS.Application.Common.Models;
using HexaPOS.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace HexaPOS.Application.Features.SaleItems
{
    public class BulkSyncSaleItemsRequest
    {
        public List<SaleItemDto> SaleItems { get; set; } = new();
    }
    public class BulkSyncSaleItemsHandler : IHandler<BulkSyncSaleItemsRequest, Result>
    {
        private readonly ISyncService _syncService;

        public BulkSyncSaleItemsHandler(ISyncService syncService)
        {
            _syncService = syncService;
        }
        public async Task<Result> HandleAsync(BulkSyncSaleItemsRequest request, CancellationToken ct)
        {
            await _syncService.SyncAsync<SaleItem, SaleItemDto>(
            request.SaleItems,
            (entity, dto) =>
            {
                entity.Id = dto.Id;
                entity.SkuSnapshot = dto.SkuSnapshot;
                entity.ProductNameSnapshot = dto.ProductNameSnapshot;
                entity.AttributeSnapshot = dto.AttributeSnapshot;
                entity.UnitPriceSnapshot = dto.UnitPriceSnapshot;
                entity.Quantity = dto.Quantity;
                entity.Subtotal = dto.Subtotal;
                entity.SaleId = dto.SaleId;
                entity.ProductId = dto.ProductId;
                entity.UpdatedAt = dto.UpdatedAt;
                entity.DeletedAt = dto.DeletedAt;
            },
            ct);

            return Result.Ok();
        }
    }
}
