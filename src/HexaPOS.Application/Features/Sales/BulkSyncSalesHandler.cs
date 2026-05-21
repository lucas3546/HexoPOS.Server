using HexaPOS.Application.Common.Interfaces;
using HexaPOS.Application.Common.Models;
using HexaPOS.Domain.Entites;
using HexaPOS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HexaPOS.Application.Features.Sales
{

    public sealed class BulkSyncSalesRequest
    {
        public List<SaleDto> Sales { get; set; } = new();
    }

    public class BulkSyncSalesHandler : IHandler<BulkSyncSalesRequest, Result>
    {
        private readonly ISyncService _syncService;

        public BulkSyncSalesHandler(ISyncService syncService)
        {
            _syncService = syncService;
        }
        public async Task<Result> HandleAsync(BulkSyncSalesRequest request, CancellationToken ct)
        {
            await _syncService.SyncAsync<Sale, SaleDto>(
            request.Sales,
            (entity, dto) =>
            {
                entity.Id = dto.Id;
                entity.SaleNumber = dto.SaleNumber;
                entity.TotalAmount = dto.TotalAmount;
                entity.PaymentType = dto.PaymentType;
                entity.UpdatedAt = dto.UpdatedAt;
                entity.DeletedAt = dto.DeletedAt;
            },
            ct);

            return Result.Ok();
        }
    }
}
