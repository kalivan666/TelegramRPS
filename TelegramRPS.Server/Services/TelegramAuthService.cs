using TelegramRPS.Server.Interfaces;
using TelegramRPS.Shared.Models.Profile;

public class TelegramAuthService : ITelegramAuthService
{
    private readonly Dictionary<long, TelegramProfile> _telegramProfiles = new(); // временно
    private readonly Dictionary<Guid, UserProfile> _userProfiles = new(); // временно

    public void UpdateUser(UserProfile profile)
    {
        _userProfiles[profile.Id] = profile;
    }

    public UserProfile GetOrCreateUser(long telegramUserId, string? firstName, string? lastName, string? username, string? photoUrl)
    {
        if (_telegramProfiles.TryGetValue(telegramUserId, out var profile))
        {
            return _userProfiles[profile.UserProfileId];
        }

        var userProfile = new UserProfile
        {
            DisplayName = username ?? $"{firstName} {lastName}".Trim(),
            AvatarUrl = photoUrl
        };

        var telegramProfile = new TelegramProfile
        {
            TelegramUserId = telegramUserId,
            FirstName = firstName,
            LastName = lastName,
            Username = username,
            PhotoUrl = photoUrl,
            UserProfileId = userProfile.Id
        };

        _telegramProfiles[telegramUserId] = telegramProfile;
        _userProfiles[userProfile.Id] = userProfile;

        return userProfile;
    }
}