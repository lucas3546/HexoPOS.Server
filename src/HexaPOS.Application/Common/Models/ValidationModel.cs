namespace HexaPOS.Application.Common.Models;

public record ValidationModel(string? errorMessage, IEnumerable<string> memberNames);
