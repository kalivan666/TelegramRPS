using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using TelegramRPS.Shared.Models.Game;

namespace TelegramRPS.Client.Services.SignalR;

public class LobbyHubClient
{
    private HubConnection? _connection;

    public event Action<Guid>? OnPlayerJoined;
    public event Action<GameLobby>? OnLobbyUpdated;

    public async Task ConnectAsync(string baseUrl)
    {
        _connection = new HubConnectionBuilder()
            .WithUrl($"{baseUrl}/lobbyhub")
            .WithAutomaticReconnect()
            .Build();

        _connection.On<Guid>("PlayerJoined", userId =>
        {
            OnPlayerJoined?.Invoke(userId);
        });

        _connection.On<GameLobby>("LobbyUpdated", lobby =>
        {
            OnLobbyUpdated?.Invoke(lobby);
        });

        await _connection.StartAsync();
    }

    public async Task JoinLobby(Guid lobbyId, Guid userProfileId)
    {
        if (_connection == null || _connection.State != HubConnectionState.Connected)
            throw new InvalidOperationException("SignalR connection is not active.");

        await _connection.InvokeAsync("JoinLobby", lobbyId, userProfileId);
    }

    public async Task DisconnectAsync()
    {
        if (_connection != null)
        {
            await _connection.StopAsync();
            await _connection.DisposeAsync();
        }
    }
}
