namespace TelegramRPS.Server.Interfaces;

public interface ITelegramBotService
{
    Task StartAsync(CancellationToken cancellationToken);
    Task StopAsync(CancellationToken cancellationToken);
}
