using TelegramRPS.Shared.Models.Profile;

namespace TelegramRPS.Server.Interfaces;

public interface ITelegramAuthService
{
    UserProfile? GetUser(int  userId);
    UserProfile GetOrCreateUser(long telegramUserId, string? firstName, 
                                string? lastName, string? username, string? photoUrl);

    void UpdateUser(UserProfile userProfile);
}
