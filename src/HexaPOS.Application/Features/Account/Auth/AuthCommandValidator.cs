using FluentValidation;

namespace HexaPOS.Application.Features.Account.Auth;

public class AuthCommandValidator : AbstractValidator<AuthRequest>
{
    public AuthCommandValidator()
    {
        RuleFor(v => v.Email)
            .MaximumLength(200)
            .EmailAddress()
            .NotEmpty();
        
        RuleFor(v => v.Password).NotEmpty()
            .MinimumLength(8)
            .MaximumLength(16)
            .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
            .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
            .Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (!? *.).");
    }
}