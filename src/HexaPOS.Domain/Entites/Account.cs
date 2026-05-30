using HexaPOS.Domain.Entites.Base;
using Microsoft.AspNetCore.Identity;

namespace HexaPOS.Domain.Entites;

public class Account : IdentityUser
{
    public string DeviceId { get; set; } = null!;
    public string? OperatingSystem { get; set; }
    public string? AppVersion { get; set; }
}