using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HexaPOS.Application.Features.Account.Register;

public class RegisterAccountCommandValidator : AbstractValidator<RegisterAccountRequest>
{
    public RegisterAccountCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(v => v.Email)
            .NotEmpty()
            .MaximumLength(200)
            .EmailAddress();

        RuleFor(v => v.Password).NotEmpty()
            .MinimumLength(8)
            .MaximumLength(16)
            .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
            .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
            .Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (!? *.).");

        RuleFor(v => v.DeviceId)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(v => v.OperatingSystem)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(v => v.AppVersion)
            .NotEmpty()
            .MaximumLength(50);
    }
}