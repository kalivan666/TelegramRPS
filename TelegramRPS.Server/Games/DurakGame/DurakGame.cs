using TelegramRPS.Shared.Models.Cards;
using TelegramRPS.Shared.Models.Game;
using TelegramRPS.Shared.Models.Profile;
using static TelegramRPS.Shared.Models.Game.GameEnums;

namespace TelegramRPS.Server.Games.DurakGame;

public class DurakGame
{
    public Guid GameId { get; private set; } = Guid.NewGuid();
    public List<GameProfile> Players { get; private set; } = new();
    public Deck Deck { get; private set; } = new Deck();
    public List<CardInTable> TableCards { get; private set; } = new();

    public int CurrentPlayerIndex { get; private set; } = 0;
    public int AttackerIndex => CurrentPlayerIndex;
    public int DefenderIndex => (CurrentPlayerIndex + 1) % Players.Count;

    public Card TrumpCard { get; private set; }

    public GameStatus State { get; private set; } = GameStatus.WaitingForPlayers;

    public DurakGame()
    {
    }

    public void AddPlayer(GameProfile player)
    {
        if (State != GameStatus.WaitingForPlayers)
            throw new InvalidOperationException("Нельзя добавить игрока после начала игры.");

        if (Players.Any(p => p.Id == player.Id))
            throw new InvalidOperationException("Игрок уже в игре.");

        Players.Add(player);
    }

    public bool CanStart => Players.Count >= 2; // можно задать лимит

    public void StartGame()
    {
        if (!CanStart)
            throw new InvalidOperationException("Недостаточно игроков для начала игры.");

        Deck.Shuffle();
        TrumpCard = Deck.GetThumpCard();

        DealInitialCards();

        State = GameStatus.InProgress;
    }

    private void DealInitialCards()
    {
        const int cardsPerPlayer = 6;

        foreach (var player in Players)
        {
            for (var i = 0; i < cardsPerPlayer; i++)
            {
                if (Deck.Cards.Count > 0)
                    player.CardsInHand.Add(Deck.DrawCard());
            }
        }
    }

    public bool AttackCard(Guid playerId, Card card)
    {
        if (State != GameStatus.InProgress) return false;

        var attacker = Players[AttackerIndex];
        if (attacker.Id != playerId) return false;
        if (!attacker.CardsInHand.Contains(card)) return false;
        if (TableCards.Count == 6) return false;

        if (TableCards.Count != 0 &&
            !TableCards.Any(c => c.AttackerCard.Rank == card.Rank || c.DefenderCard?.Rank == card.Rank))
            return false;

        TableCards.Add(new CardInTable() { AttackerCard = card });
        attacker.CardsInHand.Remove(card);

        return true;
    }

    public bool DefendCard(Guid playerId, Card attackingCard, Card defendingCard)
    {
        if (State != GameStatus.InProgress) return false;

        var defender = Players[DefenderIndex];
        if (defender.Id != playerId) return false;
        if (!defender.CardsInHand.Contains(defendingCard)) return false;

        var cardInTable = TableCards.FirstOrDefault(c => c.AttackerCard == attackingCard);
        if (cardInTable == null || !cardInTable.Defense(defendingCard, TrumpCard)) return false;

        cardInTable.DefenderCard = defendingCard;
        defender.CardsInHand.Remove(defendingCard);

        return true;
    }
}