using HexaPOS.Domain.Enums;

namespace HexaPOS.Application.Common.Models;

public abstract class BaseSyncModel
{
    public Guid Id { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }
}