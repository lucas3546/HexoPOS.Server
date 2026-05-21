using HexaPOS.Domain.Entites.Base;
using HexaPOS.Domain.Interfaces;

namespace HexaPOS.Domain.Entites;

public class Category : ISyncableEntity
{
    public Guid Id {  get; set; }

    public string Name { get; set; } = string.Empty;
    
    //Relationships
    public Guid? ParentId { get; set; }
    public Category? Parent { get; set; }
    
    public List<Product>? Products { get; set; } = new();

    public DateTimeOffset UpdatedAt { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }
}