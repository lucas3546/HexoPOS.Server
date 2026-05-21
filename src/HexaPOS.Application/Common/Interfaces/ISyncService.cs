using HexaPOS.Application.Common.Models;
using HexaPOS.Domain.Entites.Base;
using HexaPOS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace HexaPOS.Application.Common.Interfaces
{
    public interface ISyncService
    {
        Task SyncAsync<TEntity, TDto>(
        IEnumerable<TDto> items,
        Action<TEntity, TDto> apply,
        CancellationToken ct)
        where TEntity : class, ISyncableEntity, new()
        where TDto : BaseSyncModel;
    }

}