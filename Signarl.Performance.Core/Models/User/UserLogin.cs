namespace Signarl.Performance.Core.Models;

public sealed record UserLoginRequest(string Username, string Password);

public sealed record UserLoginResultModel(string UserId, string Token);