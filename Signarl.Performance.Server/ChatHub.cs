using Microsoft.AspNetCore.SignalR;
using RpcServer.Abp;
using Signarl.Performance.Core;

namespace Signarl.Performance.Server;

public sealed class ChatHub(ILogger<ChatHub> logger) : Hub
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

    [HubMethodName("login")]
    public LoginRespPackage LoginAsync(
        LoginPackage request)
    {
        return new LoginRespPackage(true);
    }
}