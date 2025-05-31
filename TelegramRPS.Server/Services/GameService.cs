using System.Collections.Concurrent;
using TelegramRPS.Server.Games.DurakGame;
using TelegramRPS.Server.Interfaces;
using TelegramRPS.Shared.Models.Cards;
using TelegramRPS.Shared.Models.Profile;

namespace TelegramRPS.Server.Services;

public class GameService : IGameService
{
    private readonly ConcurrentDictionary<Guid, DurakGame> _games = new();

    public DurakGame CreateGame(List<GameProfile> players)
    {
        var game = new DurakGame();
        _games[game.GameId] = game;
        return game;
    }

    public DurakGame? GetGame(Guid gameId)
    {
        _games.TryGetValue(gameId, out var game);
        return game;
    }

    public bool MakeMove(Guid gameId, Guid playerId, MoveType moveType, Card card, Card? defendingCard = null)
    {
        if (!_games.TryGetValue(gameId, out var game)) 
            return false;

        switch (moveType)
        {
            case MoveType.Attack:
                return game.AttackCard(playerId, card);

            case MoveType.Defend:
                if (defendingCard == null) return false;
                return game.DefendCard(playerId, card, defendingCard);
            default:
                return false;
        }
    }

    public DurakGame CreateGameFromTelegram(Guid userProfileId)
    {
        // Получаем профиль пользователя (в будущем можно будет через репозиторий/DB)
        var userProfile = new UserProfile
        {
            Id = userProfileId
            // Можно подгрузить остальное, если понадобится
        };

        // Создаём игровой профиль
        var gameProfile = new GameProfile
        {
            UserProfileId = userProfile.Id,
            IsAttacker = false,
            IsDefender = false
        };

        // Создаём игру
        var game = new DurakGame();

        // Привязываем игру к игровому профилю
        gameProfile.GameId = game.GameId;

        // Добавляем в кэш
        _games[game.GameId] = game;

        return game;
    }
}
