using FluentValidation;
using HexaPOS.Application.Common.Interfaces;
using HexaPOS.Application.Common.Models;

namespace HexaPOS.Application.Features.Account.Auth;

public class AuthRequest
{
    public string Email { get; init; } = null!;
    public string Password { get; init; } = null!;
    public string DeviceId { get; init; } = null!;
    public string OperatingSystem { get; init; } = null!;
    public string AppVersion { get; init; } = null!;
}
public class AuthCommandHandler : IHandler<AuthRequest, Result<string>>
{
    private readonly IValidator<AuthRequest> _validator;
    private readonly IIdentityService _identityService;
    private readonly IJwtService _jwtService;

    public AuthCommandHandler(IValidator<AuthRequest> validator, IIdentityService identityService, IJwtService jwtService)
    {
        _validator = validator;
        _identityService = identityService;
        _jwtService = jwtService;
    }

    public async Task<Result<string>> HandleAsync(AuthRequest request, CancellationToken ct)
    {
        var validation = await _validator.ValidateAsync(request);

        if (!validation.IsValid)
        {
            return Result<string>.Fail(
                validation.Errors
                    .Select(x => x.ErrorMessage)
                    .ToList());
        }

        var loginResult = await _identityService.LoginAsync(
            request.Email,
            request.Password,
            request.DeviceId,
            request.OperatingSystem,
            request.AppVersion,
            ct);

        if (!loginResult.Success || loginResult.Value == null)
        {
            return Result<string>.Fail(
                loginResult.Errors);
        }

        var jwt = await _jwtService.GenerateAccessTokenAsync(loginResult.Value);


        return Result<string>.Ok(jwt);
    }
}
