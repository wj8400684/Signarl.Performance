using Microsoft.AspNetCore.SignalR;
using Signarl.Performance.Server;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(kestrel =>
{
    kestrel.ListenAnyIP(5003, listen =>
    {
        listen.UseHub<ChatHub>();
    });
});

//builder.Services.AddSingleton<ChatHub>();

builder.Services.AddSignalR()
                .AddMessagePackProtocol();

var app = builder.Build();

//app.MapHub<ChatHub>("/Chat");

app.Run();