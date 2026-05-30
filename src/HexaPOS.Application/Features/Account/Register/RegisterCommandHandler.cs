using FluentValidation;
using HexaPOS.Application.Common.Interfaces;
using HexaPOS.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace HexaPOS.Application.Features.Account.Register;

public record RegisterAccountRequest(string Name, string Email, string Password, string DeviceId, string OperatingSystem, string AppVersion);

public class RegisterCommandHandler : IHandler<RegisterAccountRequest, Result>
{
    private readonly IValidator<RegisterAccountRequest> _validator;
    private readonly IIdentityService _identityService;

    public RegisterCommandHandler(IValidator<RegisterAccountRequest> validator, IIdentityService identityService)
    {
        _validator = validator;
        _identityService = identityService;
    }

    public async Task<Result> HandleAsync(RegisterAccountRequest request, CancellationToken ct)
    {
        var validation = await _validator.ValidateAsync(request);

        if (!validation.IsValid)
        {
            return Result.Fail(
                validation.Errors
                    .Select(x => x.ErrorMessage)
                    .ToArray());
        }

        var createResult = await _identityService.CreateAccountAsync(
            request.Name,
            request.Email,
            request.Password,
            request.DeviceId,
            request.OperatingSystem,
            request.AppVersion,
            ct);

        if (!createResult.Success)
        {
            return Result.Fail(createResult.Errors.ToArray());
        }

        return Result.Ok();
    }
}