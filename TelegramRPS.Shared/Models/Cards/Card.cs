using static TelegramRPS.Shared.Models.Cards.CardEnums;

namespace TelegramRPS.Shared.Models.Cards;

public class Card
{
    public Suit Suit { get; set; }
    public Rank Rank { get; set; }
}
