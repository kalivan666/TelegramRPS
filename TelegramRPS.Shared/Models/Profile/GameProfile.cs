using TelegramRPS.Shared.Models.Cards;

namespace TelegramRPS.Shared.Models.Profile;

public class GameProfile
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid UserProfileId {  get; set; }

    public List<Card> CardsInHand { get; set; } = new();

    public bool IsAttacker { get; set; }
    public bool IsDefender { get; set; }

    public Guid? GameId { get; set; }
}
