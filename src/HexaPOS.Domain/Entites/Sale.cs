using HexaPOS.Domain.Entites.Base;
using HexaPOS.Domain.Enums;

namespace HexaPOS.Domain.Entites;

public class Sale : BaseAuditableEntity<Guid>
{
    public long SaleNumber { get; set; }
    public decimal TotalAmount  { get; set; }
    public PaymentType PaymentType { get; set; }
    
    //Relationships
    public List<SaleItem> SaleItems { get; set; } = new();
}