using HexaPOS.Domain.Entites.Base;

namespace HexaPOS.Domain.Entites;

public class SaleItem : BaseAuditableEntity<Guid>
{
    public string SkuSnapshot { get; set; }
    public string ProductNameSnapshot { get; set; }
    public string AttributeSnapshot { get; set; }
    public decimal UnitPriceSnapshot  { get; set; }
    public int Quantity { get; set; }
    public decimal Subtotal { get; set; }
    
    //Relationships
    public Guid SaleId { get; set; }
    public Sale Sale { get; set; }
    
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
}