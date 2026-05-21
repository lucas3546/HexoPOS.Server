using HexaPOS.Application.Common.Interfaces;
using HexaPOS.Application.Common.Models;
using HexaPOS.Domain.Interfaces;
using HexaPOS.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HexaPOS.Infraestructure.Services
{
    public class SyncService : ISyncService
    {
        private readonly ApplicationDbContext _context;

        public SyncService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SyncAsync<TEntity, TDto>(
            IEnumerable<TDto> items,
            Action<TEntity, TDto> apply,
            CancellationToken ct)
            where TEntity : class, ISyncableEntity, new()
            where TDto : BaseSyncModel
        {
            var list = items.ToList();

            if (list.Count == 0)
                return;

            var dbSet = _context.Set<TEntity>();

            var ids = list
                .Select(x => x.Id)
                .ToList();

            var existing = await dbSet
                .Where(x => ids.Contains(x.Id))
                .ToDictionaryAsync(x => x.Id, ct);

            foreach (var dto in list)
            {
                // CREATE
                if (!existing.TryGetValue(dto.Id, out var entity))
                {
                    // no crear registros ya borrados
                    if (dto.DeletedAt != null)
                        continue;

                    entity = new TEntity();

                    apply(entity, dto);

                    dbSet.Add(entity);

                    continue;
                }

                // DELETE
                if (dto.DeletedAt != null)
                {
                    var shouldDelete =
                        entity.DeletedAt == null ||
                        dto.DeletedAt > entity.DeletedAt;

                    if (shouldDelete)
                    {
                        entity.DeletedAt = dto.DeletedAt;
                        entity.UpdatedAt = dto.UpdatedAt;
                    }

                    continue;
                }

                // UPDATE
                if (dto.UpdatedAt > entity.UpdatedAt)
                {
                    apply(entity, dto);
                }
            }

            await _context.SaveChangesAsync(ct);
        }
    }
}
