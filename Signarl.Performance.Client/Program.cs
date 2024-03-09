using System;
using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Signarl.Performance.Client;
using Signarl.Performance.Core;

var token =
    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoid3VqdW4iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjdjNzZlZmY2LWJjMDAtNGRhNi05MmEzLTI1ZWU2NjBlYmIwZiIsImp0aSI6IjIzZjNhZjQ3LWRlYWItNGEwNS1iN2IzLTcxYjYxNDgwYWM0YyIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6WyJBZG1pbiIsIkNsaWVudCJdLCJleHAiOjE3MTA1OTU1MTYsImlzcyI6Ind1anVuIiwiYXVkIjoid3VqdW4ifQ.asa4oCI4IEkuR__Q8ETNaxNu8p4phVxfGAG_08-4h98";

//
var hub = new HubConnectionBuilder()
    //.WithSocketConnectionFactory(new IPEndPoint(IPAddress.Loopback, 5003))
    .WithUrl("ws://localhost:5003/Chat", 
        option => option.AccessTokenProvider = () => Task.FromResult(token))
    .AddMessagePackProtocol();

var connection = hub.Build();

connection.On(Commands.OrderAdd, async (OrderAddRequest request) =>
{
    Console.WriteLine("收到订单");
    
    await Task.Delay(1000);

    return new OrderAddResponse(true);
});

await connection.StartAsync();

// while (true)
// {
//     await Task.Delay(1000);
//
//     await connection.InvokeAsync<LoginRespPackage>(
//         methodName: LoginCommand,
//         arg1: new LoginPackage(Username: "ssss", Password: "ddddd"));
// }

//Console.ReadKey();
//

//
var sendCount = 0;
var watch = new Stopwatch();

Console.WriteLine("开始发送数据");

watch.Start();
while (watch.Elapsed.TotalSeconds < 60)
{
    sendCount++;

    using var sources = new CancellationTokenSource(TimeSpan.FromSeconds(150));
    
    var resp = await connection.InvokeAsync<OrderAddByIdRequest>(
        methodName: Commands.OrderAdd,
        arg1: new OrderAddByIdRequest(ClientId: "ssss", UserId: "ssss"),
        cancellationToken: sources.Token);

    break;
}

watch.Stop();

Console.WriteLine($"支持完毕总共发送{sendCount}");

await Task.Delay(1000000);

Console.ReadKey();


//
// var watch = new Stopwatch();
//
// Console.WriteLine("请输入发送次数，不输入默认为500w次按enter ");
//
// var count = 1000 * 1000;
//
// var input = Console.ReadLine();
//
// if (!string.IsNullOrWhiteSpace(input)) 
//     _ = int.TryParse(input, out count);
//
// Console.WriteLine($"开始执行");
//
// watch.Restart();
//
// for (var i = 0; i < count; i++)
// {
//     await connection.InvokeAsync<LoginRespPackage>(
//         methodName: LoginCommand,
//         arg1: new LoginPackage(Username: "ssss", Password: "ddddd"));
// }
//
// watch.Stop();
//
// Console.WriteLine($"执行完成{watch.ElapsedMilliseconds/1000}秒");
//
// Console.ReadKey();