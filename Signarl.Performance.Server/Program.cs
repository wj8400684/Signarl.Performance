using RpcServer.Abp;
using Signarl.Performance.Server;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseSockets(s =>
{
    s.WaitForDataBeforeAllocatingBuffer = false;
    s.UnsafePreferInlineScheduling = false;
});

builder.Services.AddSingleton<ChatHub>();

builder.Services.AddHostedService<PackageHostServer>();

builder.Services.AddSignalR()
                .AddMessagePackProtocol();

var app = builder.Build();

app.MapHub<ChatHub>("/Chat");

app.Run();