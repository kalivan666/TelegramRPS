using TelegramRPS.Shared.Models.Game;
using TelegramRPS.Shared.Models.Profile;

namespace TelegramRPS.Server.Interfaces;

public interface IGameLobbyService
{
    GameLobby CreateLobbyFromTelegramId(long telegramUserId);
    GameLobby? GetLobby(Guid lobbyId);
    List<GameLobby> GetAllLobbies();
    bool JoinLobby(Guid lobbyId, GameProfile gameProfile);
    bool LeaveLobby(Guid lobbyId, Guid playerProfileId);
}
