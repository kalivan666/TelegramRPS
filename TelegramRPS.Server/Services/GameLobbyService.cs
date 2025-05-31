using System.Collections.Concurrent;
using TelegramRPS.Server.Interfaces;
using TelegramRPS.Shared.Models.Game;
using TelegramRPS.Shared.Models.Profile;

namespace TelegramRPS.Server.Services;

public class GameLobbyService : IGameLobbyService
{
    private readonly ConcurrentDictionary<Guid, GameLobby> _lobbies = new();

    public GameLobby CreateLobbyFromTelegramId(long telegramUserId)
    {
        var gameProfile = new GameProfile
        {
 
            UserProfileId = Guid.NewGuid(), // Можно получить из БД, пока заглушка
            IsAttacker = false,
            IsDefender = false
        };

        var lobby = new GameLobby
        {
            Players = new List<GameProfile> { gameProfile },
        };

        _lobbies[lobby.Id] = lobby;
        return lobby;
    }

    public GameLobby? GetLobby(Guid lobbyId)
    {
        _lobbies.TryGetValue(lobbyId, out var lobby);
        return lobby;
    }

    public List<GameLobby> GetAllLobbies() => _lobbies.Values.ToList();

    public bool JoinLobby(Guid lobbyId, GameProfile player)
    {
        if (!_lobbies.TryGetValue(lobbyId, out var lobby)) return false;
        //if (lobby.IsStarted) return false;

        lobby.Players.Add(player);
        return true;
    }

    public bool LeaveLobby(Guid lobbyId, Guid playerProfileId)
    {
        if (!_lobbies.TryGetValue(lobbyId, out var lobby)) return false;

        var player = lobby.Players.FirstOrDefault(p => p.UserProfileId == playerProfileId);
        if (player == null) return false;

        lobby.Players.Remove(player);
        return true;
    }
}
