namespace Signarl.Performance.Core.Models;

public sealed record RegisterRequest(string Username, string Password, string Email);

public sealed record RegisterResultModel(string UserId);