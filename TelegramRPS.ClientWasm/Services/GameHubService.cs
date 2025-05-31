using Microsoft.AspNetCore.SignalR.Client;

namespace TelegramRPS.ClientWasm.Services;

public class GameHubService
{
    private HubConnection _hubConnection;

    public event Action<Guid>? OnGameCreated;
    public event Action<Guid>? OnPlayerJoined;

    public async Task InitializeAsync(string baseUrl)
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl($"{baseUrl}/gamehub")
            .WithAutomaticReconnect()
            .Build();

        _hubConnection.On<Guid>("GameCreated", gameId =>
        {
            OnGameCreated?.Invoke(gameId);
        });

        _hubConnection.On<Guid>("PlayerJoined", userId =>
        {
            OnPlayerJoined?.Invoke(userId);
        });

        await _hubConnection.StartAsync();
    }

    public async Task<Guid> CreateGame(long userProfileId)
    {
        var result = await _hubConnection.InvokeAsync<string>("CreateGame", userProfileId);
        return Guid.Parse(result);
    }

    public async Task<bool> JoinGame(Guid gameId, Guid userProfileId)
    {
        return await _hubConnection.InvokeAsync<bool>("JoinGame", gameId, userProfileId);
    }
}