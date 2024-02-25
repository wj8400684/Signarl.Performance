using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Signarl.Performance.Core;

namespace Signarl.Performance.Server;

public interface IOrderApi
{
    Task<AddOrderResponse> AddOrderAsync(AddOrderRequest request);
}

public sealed class ChatHub(ILogger<ChatHub> logger) : Hub<IOrderApi>
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

    [HubMethodName(Commands.AddOrder)]
    public async Task<AddOrderResponse> AddOrderAsync(
        AddOrderRequest request)
    {
        var response = await Clients.Client(Context.ConnectionId).AddOrderAsync(request);

        return response;
    }
}