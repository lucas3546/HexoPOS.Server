using FluentValidation;
using HexaPOS.Application.Common.Interfaces;

namespace HexaPOS.Application.Features.Account.Auth;

public record AuthRequest(string Email, string Password);

public class AuthCommandHandler : IHandler<AuthRequest, string>
{
    private readonly IValidator<AuthRequest> _validator;

    public AuthCommandHandler(IValidator<AuthRequest> validator)
    {
        _validator = validator;
    }
    
    public async Task<string> HandleAsync(AuthRequest request, CancellationToken ct)
    {
        var result = await _validator.ValidateAsync(request);
        return "ok";
    }
}