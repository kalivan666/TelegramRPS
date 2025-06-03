namespace TelegramRPS.Shared.Models.Profile;

public class TelegramProfile
{
    public int Id { get; set; }

    public int UserProfileId { get; set; }

    public long TelegramUserId { get; set; }

    public string? Username { get; set; } = string.Empty;
    public string? FirstName {  get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public string? PhotoUrl { get; set; } = string.Empty;
}
