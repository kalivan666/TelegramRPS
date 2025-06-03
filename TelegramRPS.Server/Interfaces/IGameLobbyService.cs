using TelegramRPS.Shared.Models.DTO;
using TelegramRPS.Shared.Models.Game;
using TelegramRPS.Shared.Models.Profile;

namespace TelegramRPS.Server.Interfaces;

public interface IGameLobbyService
{
    GameLobby CreateLobby(CreateLobbyRequest request);
    GameLobby? GetLobby(int lobbyId);
    List<GameLobby> GetAllLobbies();
    bool JoinLobby(int lobbyId, GameProfile gameProfile);
    bool LeaveLobby(int lobbyId, int playerProfileId);
}
