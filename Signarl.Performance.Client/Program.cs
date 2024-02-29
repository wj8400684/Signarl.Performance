using System;
using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Signarl.Performance.Client;
using Signarl.Performance.Core;

//
var hub = new HubConnectionBuilder()
    //.WithSocketConnectionFactory(new IPEndPoint(IPAddress.Loopback, 5003))
    .WithUrl("ws://localhost:5003/Chat")
    .AddMessagePackProtocol();

var connection = hub.Build();

connection.On<OrderAddRequest, OrderAddResponse>(Commands.OrderAdd, async request =>
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