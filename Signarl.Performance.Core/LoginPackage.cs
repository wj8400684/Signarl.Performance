namespace Signarl.Performance.Core;

public record struct LoginPackage(string Username, string Password);

public record struct LoginRespPackage(bool SuccessFul = default, byte ErrorCode = default,
    string? ErrorMessage = default);