using Signarl.Performance.Server;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ChatHub>();

builder.Services.AddSignalR()
                .AddMessagePackProtocol();

var app = builder.Build();

app.MapHub<ChatHub>("/Chat");

app.Run();