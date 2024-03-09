using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Signarl.Performance.Core;
using Signarl.Performance.Server.Commands.OrderAdd;
using Signarl.Performance.Server.Data;

namespace Signarl.Performance.Server;

[Authorize]
public sealed class ChatHub(ILogger<ChatHub> logger) : Hub<IChatHubOrderApi>
{
    public override Task OnConnectedAsync()
    {
        logger.LogInformation($"客户端连接");

        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        logger.LogInformation($"客户端断开");

        return base.OnDisconnectedAsync(exception);
    }

    /// <summary>
    /// admin=>server=>client
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Authorize(Roles = UserRoles.Admin)]
    [HubMethodName(Signarl.Performance.Core.Commands.OrderAdd)]
    public async Task<OrderAddResponse> OrderAddAsync(OrderAddByIdRequest request)
    {
        var command = new OrderAddCommand(Context.ConnectionId);

        return await command.ExecuteAsync(Context.ConnectionAborted);
    }
}