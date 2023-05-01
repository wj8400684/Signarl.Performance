using Microsoft.AspNetCore.SignalR;
using Signarl.Performance.Core;

namespace Signarl.Performance.Server;

public sealed class ChatHub : Hub
{
    private readonly ILogger<ChatHub> _logger;

    public ChatHub(ILogger<ChatHub> logger)
    {
        _logger = logger;
    }

    public override Task OnConnectedAsync()
    {
        _logger.LogInformation($"客户端连接：{Context.GetHttpContext().Connection.RemoteIpAddress}-{Context.ConnectionId}");
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        _logger.LogInformation($"客户端断开：{Context.GetHttpContext().Connection.RemoteIpAddress}-{Context.ConnectionId}");
        return base.OnDisconnectedAsync(exception);
    }

    private static readonly LoginRespPackage Package = new(SuccessFul: true);

    [HubMethodName("login")]
    public ValueTask<LoginRespPackage> LoginAsync(
        LoginPackage request)
    {
        //
        return ValueTask.FromResult(Package);
    }
}