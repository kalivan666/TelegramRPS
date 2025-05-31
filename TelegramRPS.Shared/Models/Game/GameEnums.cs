namespace TelegramRPS.Shared.Models.Game;

public class GameEnums
{
    public enum GameStatus
    {
        WaitingForPlayers,
        InProgress,
        Finished
    }

    public enum GameMode
    {
        Classic,
        Transferable
    }

    public enum DeckType
    {
        TwentyFour = 24,
        TrhirtySix = 36
    }
}
