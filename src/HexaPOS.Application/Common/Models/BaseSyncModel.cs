using HexaPOS.Domain.Enums;

namespace HexaPOS.Application.Common.Models;

public abstract class BaseSyncModel
{
    public Guid Id { get; set; }    
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}