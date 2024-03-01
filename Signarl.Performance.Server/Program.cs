using FastEndpoints;
using FastEndpoints.Swagger;
using Signarl.Performance.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Signarl.Performance.Server.Data;
using Signarl.Performance.Server.Extensions;
using Signarl.Performance.Server.Options;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.ConfigureOptions<JsonOptionsSetup>();
builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<AuthenticationOptionsSetup>();

builder.Services.AddSignalR()
    .AddMessagePackProtocol();

builder.Services.AddFastEndpoints()
    .SwaggerDocument();

builder.Services.AddAuthentication()
    .AddJwtBearer();

builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddJwtTokenProvider();

var app = builder.Build();

app.UseFastEndpoints()
    .UseSwaggerGen();

app.UseAuthentication();

app.UseAuthorization();

app.MapHub<ChatHub>("/Chat");

app.Run();