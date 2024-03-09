using FastEndpoints;
using Signarl.Performance.Core;

namespace Signarl.Performance.Server.Commands.OrderAdd;

internal record struct OrderAddCommand(string ConnectionId) : ICommand<OrderAddResponse>;