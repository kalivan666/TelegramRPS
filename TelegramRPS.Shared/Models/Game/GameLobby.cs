using TelegramRPS.Shared.Models.Profile;
using static TelegramRPS.Shared.Models.Game.GameEnums;

namespace TelegramRPS.Shared.Models.Game;

public class GameLobby
{
    public int Id { get; set; }

    public List<GameProfile> Players { get; set; } = new();
    public int AdminUserId { get; set; }

    public GameStatus Status { get; set; } = GameStatus.WaitingForPlayers;

    public int MaxPlayers { get; set; } = 2;

    public DateTime CreateAt { get; set; } = DateTime.UtcNow;

    public GameMode Mode { get; set; } = GameMode.Classic;

    public DeckType Deck { get; set; } = DeckType.TrhirtySix;
}
