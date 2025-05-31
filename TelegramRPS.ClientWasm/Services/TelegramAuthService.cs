using TelegramRPS.Shared.Models;
using TelegramRPS.Shared.Models.Profile;

namespace TelegramRPS.ClientWasm.Services;

public class TelegramAuthService
{
    private readonly AuthHubService _authHubService;

    public TelegramUser? CurrentTelegramUser { get; private set; }

    public event Action? OnUserChanged;

    public TelegramAuthService(AuthHubService authHubService)
    {
        _authHubService = authHubService;
    }

    public async Task SetTelegramUser(TelegramUser user)
    {
        CurrentTelegramUser = user;
        NotifyUserChanged();
    }

    private void NotifyUserChanged()
    {
        OnUserChanged?.Invoke();
    }
}
