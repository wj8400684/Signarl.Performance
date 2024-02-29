using Signarl.Performance.Core;

namespace Signarl.Performance.Server;

public interface IChatHubOrderApi
{
    Task<OrderAddResponse> OrderAddAsync(OrderAddRequest request, CancellationToken cancellationToken = default);
}