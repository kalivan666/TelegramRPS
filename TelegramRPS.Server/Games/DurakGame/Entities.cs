using TelegramRPS.Shared.Models.Cards;
using static TelegramRPS.Shared.Models.Cards.CardEnums;

namespace TelegramRPS.Server.Games.DurakGame;

public class Deck
{
    public List<Card> Cards { get; private set; } = new();

    public Deck()
    {
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                Cards.Add(new Card { Rank = rank, Suit = suit});
    }

    public void Shuffle()
    {
        var rnd = new Random();
        Cards = [.. Cards.OrderBy(c => rnd.Next())];
    }

    public Card? DrawCard()
    {
        if (Cards.Count == 0) return null;

        var card = Cards[0];
        Cards.RemoveAt(0);
        return card;
    }

    public Card GetThumpCard() => Cards.Last();
}
