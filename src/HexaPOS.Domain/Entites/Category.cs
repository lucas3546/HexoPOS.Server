using HexaPOS.Domain.Entites.Base;

namespace HexaPOS.Domain.Entites;

public class Category : BaseAuditableEntity<Guid>
{
    public string Name { get; set; } = string.Empty;
    
    //Relationships
    public Guid? ParentId { get; set; }
    public Category? Parent { get; set; }
    
    public List<Product> Products { get; set; } = new();
}