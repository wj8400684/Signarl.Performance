namespace Signarl.Performance.Core.Models;

public sealed record LoginRequest(string Username, string Password);

public sealed record LoginResultModel(string UserId, string Token);