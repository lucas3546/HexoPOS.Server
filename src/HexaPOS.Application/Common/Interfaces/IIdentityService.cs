using HexaPOS.Application.Common.Models;
using HexaPOS.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace HexaPOS.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<Result<Account>> CreateAccountAsync(
        string name,
        string email,
        string password,
        string deviceId,
        string operatingSystem,
        string appVersion,
        CancellationToken ct = default);

        Task<Result<Account>> LoginAsync(
        string email,
        string password,
        string deviceId,
        string operatingSystem,
        string appVersion,
        CancellationToken ct = default);
    }
}
