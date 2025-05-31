using Microsoft.Extensions.Hosting;
using TelegramRPS.Server.Interfaces;

namespace TelegramRPS.Server.HostedServices;

public class TelegramBotHostedService : IHostedService
{
    private readonly ITelegramBotService botService;

    public TelegramBotHostedService(ITelegramBotService botService)
    {
        this.botService = botService;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return botService.StartAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return botService.StopAsync(cancellationToken);
    }
}
