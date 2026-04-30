namespace HexaPOS.Domain.Entites.Base;

public class BaseEntity<TKey>
{
    public TKey Id { get; set; }
}