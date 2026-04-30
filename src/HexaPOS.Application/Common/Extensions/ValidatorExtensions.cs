using FluentValidation.Results;
using HexaPOS.Application.Common.Models;

namespace FluentValidation;


public static class ValidatorExtensions
{
    public static Result ToResult(this ValidationResult validationResult)
    {
        if (validationResult.IsValid)
            return Result.Ok();

        var errors = validationResult.Errors
            .Select(e => e.ErrorMessage)
            .ToArray();

        return Result.Fail(errors);
    }
}