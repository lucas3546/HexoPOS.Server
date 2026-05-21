using HexaPOS.Domain.Entites.Base;
using HexaPOS.Domain.Enums;
using HexaPOS.Domain.Interfaces;

namespace HexaPOS.Domain.Entites;

public class Sale : ISyncableEntity
{
    public Guid Id { get; set; }
    public long SaleNumber { get; set; }
    public decimal TotalAmount  { get; set; }
    public PaymentType PaymentType { get; set; }
    
    //Relationships
    public List<SaleItem>? SaleItems { get; set; } = new();


    public DateTimeOffset UpdatedAt { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }
}