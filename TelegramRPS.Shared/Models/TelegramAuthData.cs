using System.Text.Json.Serialization;

namespace TelegramRPS.Shared.Models;

public class TelegramUser
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("is_bot")]
    public bool IsBot { get; set; }

    [JsonPropertyName("first_name")]
    public string? FirstName {  get; set; }

    [JsonPropertyName("last_name")]
    public string? LastName { get; set; }

    [JsonPropertyName("username")]
    public string? Username {  get; set; }

    [JsonPropertyName("language_code")]
    public string? LanguageCode {  get; set; }

    [JsonPropertyName("photo_url")]
    public string? PhotoUrl { get; set; }
}

public class TelegramAuthData
{
    [JsonPropertyName("initData")]
    public string InitData { get; set; } = string.Empty;

    [JsonPropertyName("user")]
    public TelegramUser User { get; set; } = new TelegramUser();
}
