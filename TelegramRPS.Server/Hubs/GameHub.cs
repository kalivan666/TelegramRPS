using Microsoft.AspNetCore.SignalR;
using TelegramRPS.Server.Interfaces;
using TelegramRPS.Shared.Models.Profile;

namespace TelegramRPS.Server.Hubs;

public class GameHub : Hub
{
    private readonly IGameService _gameService;

    public GameHub(IGameService gameService)
    {
        _gameService = gameService;
    }

    public async Task<string> CreateGame(Guid userProfileId)
    {
        var game = _gameService.CreateGameFromTelegram(userProfileId);

        // Добавим пользователя в группу по GameId
        await Groups.AddToGroupAsync(Context.ConnectionId, game.GameId.ToString());

        // Можно отправить состояние обратно
        await Clients.Caller.SendAsync("GameCreated", game.GameId);

        return game.GameId.ToString();
    }

    public async Task<bool> JoinGame(Guid gameId, Guid userProfileId)
    {
        var game = _gameService.GetGame(gameId);
        if (game == null) return false;

        // Создаём нового игрока
        var gameProfile = new GameProfile
        {
            UserProfileId = userProfileId,
            GameId = gameId
        };

        game.Players.Add(gameProfile);

        await Groups.AddToGroupAsync(Context.ConnectionId, gameId.ToString());

        await Clients.Group(gameId.ToString()).SendAsync("PlayerJoined", userProfileId);

        return true;
    }
}