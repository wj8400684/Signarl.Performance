using System.Diagnostics;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Signarl.Performance.Core;

var connection = new HubConnectionBuilder()
    .WithUrl("http://localhost:5000/Chat")
    .AddMessagePackProtocol()
    .Build();

const string LoginCommand = "login";

await connection.StartAsync();

var watch = new Stopwatch();

Console.WriteLine("请输入发送次数，不输入默认为500w次按enter ");

var count = 1000 * 1000;

var input = Console.ReadLine();

if (!string.IsNullOrWhiteSpace(input)) 
    _ = int.TryParse(input, out count);

Console.WriteLine($"开始执行");

watch.Restart();

for (var i = 0; i < count; i++)
{
    await connection.InvokeAsync<LoginRespPackage>(
        methodName: LoginCommand,
        arg1: new LoginPackage(Username: "ssss", Password: "ddddd"));
}

watch.Stop();

Console.WriteLine($"执行完成{watch.ElapsedMilliseconds/1000}秒");

Console.ReadKey();