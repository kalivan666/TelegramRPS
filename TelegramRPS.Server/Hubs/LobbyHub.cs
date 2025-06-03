using Microsoft.AspNetCore.SignalR;
using Telegram.Bot.Types;
using TelegramRPS.Server.Interfaces;
using TelegramRPS.Shared.Models.Game;
using TelegramRPS.Shared.Models.Profile;
using static TelegramRPS.Shared.Models.Game.GameEnums;

public class LobbyHub : Hub
{
    private readonly IGameLobbyService _lobbyService;
    private readonly ITelegramAuthService _authService;

    public LobbyHub(IGameLobbyService lobbyService, ITelegramAuthService authService)
    {
        _lobbyService = lobbyService;
        _authService = authService;
    }

    public async Task JoinLobby(int lobbyId, int userProfileId)
    {
        var lobby = _lobbyService.GetLobby(lobbyId);
        if (lobby == null)
        {
            throw new HubException("Lobby not found");
        }

        if (!lobby.Players.Any(p => p.UserProfileId == userProfileId))
        {
            var user = _authService.GetUser(userProfileId);
            lobby.Players.Add(new GameProfile
            {
                UserProfileId = userProfileId,
                DisplayName = user.DisplayName,
                PhotoUrl = user.AvatarUrl,
                GameId = lobby.Id
            });
        }

        // Если админ отсутствует — назначаем текущего
        if (lobby.AdminUserId == 0)
        {
            lobby.AdminUserId = userProfileId;
        }

        await Groups.AddToGroupAsync(Context.ConnectionId, lobbyId.ToString());

        await Clients.Group(lobbyId.ToString()).SendAsync("PlayerJoined", userProfileId);
        await Clients.Group(lobbyId.ToString()).SendAsync("LobbyUpdated", lobby);
    }

    public async Task UpdateLobbySettings(GameLobby updatedLobby)
    {
        var lobby = _lobbyService.GetLobby(updatedLobby.Id);
        if (lobby == null)
        {
            throw new HubException("Lobby not found");
        }

        lobby.Mode = updatedLobby.Mode;
        lobby.Deck = updatedLobby.Deck;
        lobby.MaxPlayers = updatedLobby.MaxPlayers;

        // Рассылаем обновлённое состояние всем в группе
        await Clients.Group(lobby.Id.ToString()).SendAsync("LobbyUpdated", lobby);
    }

    public async Task UpdateLobbyUserReady(int lobbyId, int userId, bool isReady)
    {
        var lobby = _lobbyService.GetLobby(lobbyId);
        if (lobby == null)
        {
            throw new HubException("Lobby not found");
        }

        lobby.Players.First(p => p.UserProfileId == userId).IsReady = isReady;

        await Clients.Group(lobby.Id.ToString()).SendAsync("LobbyUpdated", lobby);
    }
    public async Task LeaveLobby(int lobbyId, int userId)
    {
        var lobby = _lobbyService.GetLobby(lobbyId);
        if (lobby == null)
            return;

        var player = lobby.Players.FirstOrDefault(p => p.UserProfileId == userId);
        if (player != null)
            lobby.Players.Remove(player);

        // Передача прав, если был админом
        if (lobby.AdminUserId == userId)
            lobby.AdminUserId = lobby.Players.Count == 0 ? 0 : lobby.Players.First().UserProfileId;

        // Рассылаем обновление всем
        await Clients.Group(lobbyId.ToString()).SendAsync("LobbyUpdated", lobby);

        // Удаляем из группы SignalR
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, lobbyId.ToString());
    }
}
