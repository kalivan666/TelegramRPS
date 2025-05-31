using Microsoft.AspNetCore.SignalR;
using TelegramRPS.Server.Interfaces;
using TelegramRPS.Shared.Models.Profile;

namespace TelegramRPS.Server.Hubs;

public class AuthHub : Hub
{
    private readonly ITelegramAuthService _authService;
    private readonly ILogger<AuthHub> _logger;

    public AuthHub(ITelegramAuthService authService, ILogger<AuthHub> logger)
    {

        _authService = authService;
        _logger = logger;
    }

    public Task<UserProfile> AuthenticateAsync(long telegramUserId, string? firstName, string? lastName, string? username, string? photoUrl)
    {
        var userProfile = _authService.GetOrCreateUser(telegramUserId, firstName, lastName, username, photoUrl);
        return Task.FromResult(userProfile);
    }
    
    public void UpdateUserProfile(UserProfile profile)
    {
        _authService.UpdateUser(profile);
    }
}