using System.Collections.Concurrent;
using TelegramRPS.Server.Interfaces;
using TelegramRPS.Shared.Models.DTO;
using TelegramRPS.Shared.Models.Game;
using TelegramRPS.Shared.Models.Profile;
using static TelegramRPS.Shared.Models.Game.GameEnums;

namespace TelegramRPS.Server.Services;

public class GameLobbyService : IGameLobbyService
{
    private int _nextLobbyId = 1;
    private readonly ConcurrentDictionary<int, GameLobby> _lobbies = new();

    public GameLobby CreateLobby(CreateLobbyRequest request)
    {
        var lobby = new GameLobby
        {
            Id = _nextLobbyId++,
            AdminUserId = request.UserProfileId,
            Mode = request.Mode,
            Deck = request.Deck,
            MaxPlayers = request.MaxPlayers,
            Status = GameStatus.WaitingForPlayers
        };

        lobby.Players.Add(new GameProfile
        {
            UserProfileId = request.UserProfileId,
            DisplayName = request.DisplayName,
            PhotoUrl = request.PhotoUrl,
            GameId = lobby.Id
        });

        _lobbies[lobby.Id] = lobby;
        return lobby;
    }

    public GameLobby? GetLobby(int lobbyId)
    {
        _lobbies.TryGetValue(lobbyId, out var lobby);
        return lobby;
    }

    public List<GameLobby> GetAllLobbies() => _lobbies.Values.ToList();

    public bool JoinLobby(int lobbyId, GameProfile player)
    {
        if (!_lobbies.TryGetValue(lobbyId, out var lobby)) return false;

        lobby.Players.Add(player);
        return true;
    }

    public bool LeaveLobby(int lobbyId, int playerProfileId)
    {
        if (!_lobbies.TryGetValue(lobbyId, out var lobby)) return false;

        var player = lobby.Players.FirstOrDefault(p => p.UserProfileId == playerProfileId);
        if (player == null) return false;

        lobby.Players.Remove(player);
        return true;
    }
}
