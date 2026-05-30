using HexaPOS.Domain.Entites;
using System.Security.Claims;

namespace HexaPOS.Application.Common.Interfaces
{
    public interface IJwtService
    {
        Task<string> GenerateAccessTokenAsync(Account account);
    }
}