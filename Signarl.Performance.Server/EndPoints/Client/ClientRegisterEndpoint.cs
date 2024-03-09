using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Signarl.Performance.Core.Models;

namespace Signarl.Performance.Server.EndPoints;

[AllowAnonymous]
[HttpGet("api/client/register")]
public sealed class ClientRegisterEndpoint : Endpoint<ClientRegisterRequest, ClientRegisterResultModel>
{
    public override Task<ClientRegisterResultModel> ExecuteAsync(ClientRegisterRequest req, CancellationToken ct)
    {
        return base.ExecuteAsync(req, ct);
    }
}