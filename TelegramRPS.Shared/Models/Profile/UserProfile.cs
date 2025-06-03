namespace TelegramRPS.Shared.Models.Profile;

public class UserProfile
{
    public int Id { get; set; }

    public string DisplayName { get; set; } = string.Empty;
    public string? FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; } = string.Empty;

    public UserGameStats GameStats { get; set; } = new();

    public DateTime RefistrationAt { get; set; } = DateTime.UtcNow;

    public int Coins { get; set; } = 0;
    public int Gems { get; set; } = 0;
}
