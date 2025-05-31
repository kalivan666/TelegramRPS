using TelegramRPS.Server.HostedServices;
using TelegramRPS.Server.Hubs;
using TelegramRPS.Server.Interfaces;
using TelegramRPS.Server.Services;

var builder = WebApplication.CreateBuilder(args);


var botToken = "7669863121:AAGJBnzCjmQY_Gieer4QbGKPhlLJ_HpeZVk";
builder.Services.AddSingleton<ITelegramBotService>(sp => new TelegramBotService(botToken));
builder.Services.AddHostedService<TelegramBotHostedService>();

builder.Services.AddSingleton<IGameService, GameService>();
builder.Services.AddSingleton<IGameLobbyService, GameLobbyService>();
builder.Services.AddSingleton<ITelegramAuthService, TelegramAuthService>();

builder.Services.AddControllers();
builder.Services.AddSignalR();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Information);

builder.WebHost.UseUrls("http://0.0.0.0:5000");
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
var app = builder.Build();

// Подключаем маршрутизацию для контроллеров
app.MapControllers();
app.UseRouting();
app.UseStaticFiles();
app.UseCors("ClientPolicy");
Console.WriteLine("terst");

app.MapHub<GameHub>("/gamehub");
app.MapHub<AuthHub>("/authhub");


var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("== Custom logger in Program.cs is working ==");

app.Run();