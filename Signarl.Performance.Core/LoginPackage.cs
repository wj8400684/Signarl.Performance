namespace Signarl.Performance.Core;

public sealed record LoginPackage(string Username, string Password);

public sealed record LoginRespPackage(bool SuccessFul = default, byte ErrorCode = default,
    string? ErrorMessage = default);