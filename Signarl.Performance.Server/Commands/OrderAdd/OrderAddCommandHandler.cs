using FastEndpoints;
using Microsoft.AspNetCore.SignalR;
using Signarl.Performance.Core;

namespace Signarl.Performance.Server.Commands.OrderAdd;

internal sealed class OrderAddCommandHandler(IHubContext<ChatHub, IChatHubOrderApi> hubContext)
    : ICommandHandler<OrderAddCommand, OrderAddResponse>
{
    public async Task<OrderAddResponse> ExecuteAsync(OrderAddCommand command, CancellationToken ct)
    {
        var resp = await hubContext.Clients.Client(command.ConnectionId)
            .OrderAddAsync(new OrderAddRequest());

        return resp;
    }
}