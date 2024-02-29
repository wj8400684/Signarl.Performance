using Microsoft.AspNetCore.SignalR;
using Signarl.Performance.Core;

namespace Signarl.Performance.Server;

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

    [HubMethodName(Commands.OrderAdd)]
    public async Task<OrderAddResponse> OrderAddAsync(OrderAddByIdRequest request)
    {
        using var sources = new CancellationTokenSource(TimeSpan.FromSeconds(15));
        
        return await Clients.Client(Context.ConnectionId)
                            .OrderAddAsync(new OrderAddRequest(), sources.Token);
    }
}