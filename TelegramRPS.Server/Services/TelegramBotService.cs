using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramRPS.Server.Interfaces;

namespace TelegramRPS.Server.Services;

public class TelegramBotService : ITelegramBotService
{
    private readonly TelegramBotClient botClient;
    private CancellationTokenSource? cts;

    public TelegramBotService(string botToken)
    {
        botClient = new TelegramBotClient(botToken);
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        botClient.StartReceiving(
            updateHandler: HandleUpdateAsync,
            pollingErrorHandler: HandleUpdateErrorAsync,
            cancellationToken: cts.Token);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        cts?.Cancel();
        return Task.CompletedTask;
    }

    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Message?.Text == "/start")
        {
            var webAppUrl = "https://telegramrps.ru";

            var inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
    InlineKeyboardButton.WithWebApp("Играть в игру", new WebAppInfo { Url = webAppUrl })
});

            await botClient.SendTextMessageAsync(
                chatId: update.Message.Chat.Id,
                text: "Нажмите, чтобы открыть игру:",
                replyMarkup: inlineKeyboard,
                cancellationToken: cancellationToken
            );
        }
    }

    private Task HandleUpdateErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
