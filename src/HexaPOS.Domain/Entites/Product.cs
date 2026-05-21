using HexaPOS.Domain.Entites.Base;
using HexaPOS.Domain.Interfaces;

namespace HexaPOS.Domain.Entites;

public class Product : ISyncableEntity
{
    public Guid Id { get; set; }
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


    public DateTimeOffset UpdatedAt {  get; set; }

    public DateTimeOffset? DeletedAt {  get; set; }
}