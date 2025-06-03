using static TelegramRPS.Shared.Models.Game.GameEnums;

namespace TelegramRPS.Shared.Models.DTO;

public class CreateLobbyRequest
{
    public int UserProfileId { get; set; }
    public string DisplayName { get; set; }
    public string PhotoUrl {  get; set; }
    public GameMode Mode { get; set; } = GameMode.Classic;
    public DeckType Deck { get; set; } = DeckType.TrhirtySix;
    public int MaxPlayers { get; set; } = 2;
}

