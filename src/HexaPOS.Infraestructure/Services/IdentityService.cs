using HexaPOS.Application.Common.Interfaces;
using HexaPOS.Application.Common.Models;
using HexaPOS.Domain.Constants;
using HexaPOS.Domain.Entites;
using Microsoft.AspNetCore.Identity;

namespace HexaPOS.Infraestructure.Services;



public class IdentityService : IIdentityService
{
    private readonly UserManager<Account> _userManager;

    public IdentityService(UserManager<Account> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<Account>> CreateAccountAsync(
        string name,
        string email,
        string password,
        string deviceId,
        string operatingSystem,
        string appVersion,
        CancellationToken ct = default)
    {
        var normalizedEmail = email
            .Trim()
            .ToLowerInvariant();

        var exists = await _userManager.FindByEmailAsync(normalizedEmail);

        if (exists is not null)
        {
            return Result<Account>.Fail(
                "An account already exists with that email address.");
        }

        var account = new Account
        {
            UserName = normalizedEmail,
            Email = normalizedEmail,

            DeviceId = deviceId,

            OperatingSystem = null,
            AppVersion = null
        };


        var result = await _userManager.CreateAsync(
            account,
            password);

        if (!result.Succeeded)
        {
            return Result<Account>.Fail(
                result.Errors
                    .Select(x => x.Description)
                    .ToList());
        }

        await _userManager.AddToRoleAsync(account, Roles.Casher);


        return Result<Account>.Ok(account);
    }

    public async Task<Result<Account>> LoginAsync(
        string email,
        string password,
        string deviceId,
        string operatingSystem,
        string appVersion,
        CancellationToken ct = default)
    {
        var normalizedEmail = email
            .Trim()
            .ToLowerInvariant();

        var account = await _userManager
            .FindByEmailAsync(normalizedEmail);

        if (account is null)
        {
            return Result<Account>.Fail(
                "Incorrect email address or password.");
        }

        var validPassword = await _userManager
            .CheckPasswordAsync(account, password);

        if (!validPassword)
        {
            return Result<Account>.Fail(
                "Incorrect email address or password.");
        }

        account.DeviceId = deviceId;
        account.OperatingSystem = operatingSystem;
        account.AppVersion = appVersion;

        await _userManager.UpdateAsync(account);

        return Result<Account>.Ok(account);
    }
}
