namespace HexaPOS.Application.Common.Models;

public sealed class Result<T>
{
    public bool Success { get; }
    public T? Value { get; }
    public IReadOnlyCollection<string> Errors { get; }

    private Result(bool success, T? value, IReadOnlyCollection<string> errors)
    {
        Success = success;
        Value = value;
        Errors = errors;
    }

    public static Result<T> Ok(T value)
        => new(true, value, Array.Empty<string>());

    public static Result<T> Fail(params string[] errors)
        => new(false, default, errors);

    public static Result<T> Fail(IEnumerable<string> errors)
        => new(false, default, errors.ToArray());

    public static Result<T> Fail(string error)
        => new(false, default, new[] { error });
}

public sealed class Result
{
    private static readonly Result _success = new(true, Array.Empty<string>());

    public bool Success { get; }
    public IReadOnlyCollection<string> Errors { get; }

    private Result(bool success, IReadOnlyCollection<string> errors)
    {
        Success = success;
        Errors = errors;
    }

    public static Result Ok()
        => _success;

    public static Result Fail(params string[] errors)
        => new(false, errors);
}