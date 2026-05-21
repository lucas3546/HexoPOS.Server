using HexaPOS.Domain.Entites.Base;

namespace HexaPOS.Domain.Entites;

public class Account : BaseEntity<Guid>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
}