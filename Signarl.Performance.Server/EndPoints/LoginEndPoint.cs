using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Signarl.Performance.Core.Models;

namespace Signarl.Performance.Server.EndPoints;

[AllowAnonymous]
[HttpGet("/api/user/login")]
public sealed class LoginEndPoint : Endpoint<LoginRequest, ApiResponse<LoginResponse>>
{
    public override async Task<ApiResponse<LoginResponse>> ExecuteAsync(LoginRequest req, CancellationToken ct)
    {
        await Task.Delay(1000, ct);

        return new LoginResponse("", "");
    }
}