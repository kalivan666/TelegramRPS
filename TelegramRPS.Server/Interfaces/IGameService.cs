using TelegramRPS.Server.Games.DurakGame;
using TelegramRPS.Shared.Models.Cards;
using TelegramRPS.Shared.Models.Profile;

namespace TelegramRPS.Server.Interfaces;

public enum MoveType
{
    Attack,
    Defend
}

public interface IGameService
{
    public DurakGame CreateGameFromTelegram(Guid userProfileId);
    DurakGame CreateGame(List<GameProfile> players);
    DurakGame? GetGame(Guid gameId);
    bool MakeMove(Guid gameId, Guid playerId, MoveType moveType, Card card, Card? defendingCard = null);
}
