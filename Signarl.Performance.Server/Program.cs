using System.Net;
using Bedrock.Framework;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Signarl.Performance.Server;

// Manual wire up of the server
var services = new ServiceCollection();
services.AddLogging(builder =>
{
    // builder.SetMinimumLevel(LogLevel.Debug);
    builder.AddConsole();
});

services.AddSignalR().AddMessagePackProtocol();

var serviceProvider = services.BuildServiceProvider();

var server = new ServerBuilder(serviceProvider)
    .ListenNamedPipe(new NamedPipeEndPoint("ss"), listen => listen.UseHub<ChatHub>())
    .Build();

var logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<Program>();

await server.StartAsync();

foreach (var ep in server.EndPoints)
    logger.LogInformation("Listening on {EndPoint}", ep);

var tcs = new TaskCompletionSource();
Console.CancelKeyPress += (sender, e) => tcs.TrySetResult();
await tcs.Task;

await server.StopAsync();