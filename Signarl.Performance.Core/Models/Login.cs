namespace Signarl.Performance.Core.Models;

public sealed record LoginRequest(string Username, string Password);

public sealed record LoginResponse(string UserId, string Token);