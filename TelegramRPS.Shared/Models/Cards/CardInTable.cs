namespace TelegramRPS.Shared.Models.Cards;

public class CardInTable
{
    public Card AttackerCard { get; set; }
    public Card DefenderCard { get; set; }

    public bool Defense(Card card, Card trumpCard)
    {
        if (AttackerCard.Suit == trumpCard.Suit)
        {
            if (card.Suit == trumpCard.Suit
                && (int)card.Rank > (int)AttackerCard.Rank)
                return true;
        }
        else
        {
            if (card.Suit == trumpCard.Suit 
                || (card.Suit == AttackerCard.Suit && (int)card.Rank > (int)AttackerCard.Rank))
                return true;
        }
        return false;
    }
}
