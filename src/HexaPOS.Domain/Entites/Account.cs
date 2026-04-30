using HexaPOS.Domain.Entites.Base;

namespace HexaPOS.Domain.Entites;

public class Account : BaseAuditableEntity<Guid>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
}