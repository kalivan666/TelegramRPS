using TelegramRPS.Shared.Models.Cards;

namespace TelegramRPS.Shared.Models.Profile;

public class GameProfile
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public int UserProfileId {  get; set; }

    public string DisplayName { get; set; }

    public string PhotoUrl {  get; set; }

    public bool IsReady { get; set; } = false;

    public List<Card> CardsInHand { get; set; } = new();

    public bool IsAttacker { get; set; }
    public bool IsDefender { get; set; }

    public int GameId { get; set; }
}
