using Microsoft.AspNetCore.HttpOverrides;
using TelegramRPS.Server.HostedServices;
using TelegramRPS.Server.Hubs;
using TelegramRPS.Server.Interfaces;
using TelegramRPS.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// === Сервисы ===
var botToken = "7669863121:AAGJBnzCjmQY_Gieer4QbGKPhlLJ_HpeZVk";
builder.Services.AddSingleton<ITelegramBotService>(_ => new TelegramBotService(botToken));
builder.Services.AddHostedService<TelegramBotHostedService>();

builder.Services.AddSingleton<IGameService, GameService>();
builder.Services.AddSingleton<IGameLobbyService, GameLobbyService>();
builder.Services.AddSingleton<ITelegramAuthService, TelegramAuthService>();

builder.Services.AddControllers();
builder.Services.AddSignalR();

// === CORS ===
builder.Services.AddCors(options =>
{
    options.AddPolicy("ClientPolicy", policy =>
    {
        policy
            .WithOrigins("https://telegramrps.ru")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// === Forwarded Headers (для прокси: nginx, IIS) ===
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownNetworks.Clear(); // если known networks/proxies известны — добавь вручную
    options.KnownProxies.Clear();
});

// === Логгирование ===
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Information);

// === Слушать на всех интерфейсах ===
builder.WebHost.UseUrls("http://0.0.0.0:5000");

var app = builder.Build();

// === Middleware порядок ВАЖЕН ===

// Используем проксированные заголовки
app.UseForwardedHeaders();

// CORS — до маршрутизации!
app.UseCors("ClientPolicy");

app.UseRouting();

// Статические файлы
app.UseStaticFiles();

// Controllers и SignalR хабы
app.MapControllers();
app.MapHub<GameHub>("/gamehub");
app.MapHub<AuthHub>("/authhub");

var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("== Custom logger in Program.cs is working ==");

app.Run();
