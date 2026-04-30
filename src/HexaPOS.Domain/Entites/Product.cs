using HexaPOS.Domain.Entites.Base;

namespace HexaPOS.Domain.Entites;

public class Product : BaseAuditableEntity<Guid>
{
    public string Name { get; set; }
    public string Ubication { get; set; }
    public string Sku { get; set; } //Unique
    public string Barcode { get; set; }
    public string Attribute { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int StockLimit { get; set; } 
    
    //Relationships
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    
}