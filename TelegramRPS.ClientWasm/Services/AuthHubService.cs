using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using TelegramRPS.Shared.Models.Profile;

namespace TelegramRPS.ClientWasm.Services;

public class AuthHubService
{
    private readonly HubConnection _hubConnection;

    public AuthHubService()
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl("https://api.telegramrps.ru/authhub")
            .WithAutomaticReconnect()
            .Build();
    }

    public async Task StartAsync()
    {
        if (_hubConnection.State == HubConnectionState.Disconnected)
        {
            await _hubConnection.StartAsync();
        }
    }

    public async Task<UserProfile?> AuthenticateAsync(long telegramUserId, string? firstName, string? lastName, string? username, string? photoUrl)
    {
        await StartAsync();

        return await _hubConnection.InvokeAsync<UserProfile>(
            "AuthenticateAsync", telegramUserId, firstName, lastName, username, photoUrl
        );
    }

    public async Task UpdateUserProfileAsync(UserProfile userProfile)
    {
        await _hubConnection.InvokeAsync("UpdateUserProfile", userProfile);
    }
}