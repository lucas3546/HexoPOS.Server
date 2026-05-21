using HexaPOS.Application.Common.Interfaces;
using HexaPOS.Application.Common.Models;
using HexaPOS.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace HexaPOS.Application.Features.Categories
{
    public class BulkSyncCategoriesRequest
    {
        public List<CategoryDto> Categories { get; set; } = new();
    }

    public class BulkSyncCategoriesHandler : IHandler<BulkSyncCategoriesRequest, Result>
    {
        private readonly ISyncService _syncService;
        public BulkSyncCategoriesHandler(ISyncService syncService)
        {
            _syncService = syncService;
        }

        public async Task<Result> HandleAsync(BulkSyncCategoriesRequest request, CancellationToken ct)
        {
            await _syncService.SyncAsync<Category, CategoryDto>(
            request.Categories,
            (entity, dto) =>
            {
                entity.Id = dto.Id;
                entity.Name = dto.Name;
                entity.ParentId = dto.ParentId;
                entity.UpdatedAt = dto.UpdatedAt;
                entity.DeletedAt = dto.DeletedAt;
            },
            ct);

            return Result.Ok();
        }
    }
}
