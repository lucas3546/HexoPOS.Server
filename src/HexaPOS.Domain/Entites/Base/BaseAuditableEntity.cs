using HexaPOS.Domain.Interfaces;

namespace HexaPOS.Domain.Entites.Base;


public class BaseAuditableEntity<T> : BaseEntity<T>, IAuditableEntity
{
    public DateTimeOffset CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
    
}