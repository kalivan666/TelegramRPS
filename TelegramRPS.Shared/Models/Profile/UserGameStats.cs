namespace TelegramRPS.Shared.Models.Profile;

public class UserGameStats
{
    public int GamesPlayed { get; set; } = 0;
    public int Wins { get; set; } = 0;
    public int Losses { get; set; } = 0;

    public double WinRate =>
        GamesPlayed == 0 ? 0 : Math.Round((double)Wins / GamesPlayed * 100, 2);

    public int Rating { get; set; } = 0;
}
