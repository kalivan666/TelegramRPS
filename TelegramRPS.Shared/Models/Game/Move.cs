using TelegramRPS.Shared.Models.Cards;

namespace TelegramRPS.Shared.Models.Game;

public class Move
{
    public Guid Id {  get; set; } = Guid.NewGuid();

    public Guid GameId { get; set; }

    public Guid PlayerId {  get; set; }

    public List<Card> PlayedCards { get; set; } = new();

    public DateTime MoveTime {  get; set; } = DateTime.UtcNow;
}
