namespace Signarl.Performance.Core.Models;

public sealed record UserRegisterRequest(string Username, string Password, string Email);

public sealed record UserRegisterResultModel(string UserId);