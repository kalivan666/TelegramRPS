using Microsoft.AspNetCore.Mvc;
using TelegramRPS.Server.Interfaces;
using TelegramRPS.Server.Services;
using TelegramRPS.Shared.Models.DTO;
using TelegramRPS.Shared.Models.Game;

namespace TelegramRPS.Server.Controllers;

[ApiController]
[Route("api/lobby")]
public class LobbyController : ControllerBase
{
    private readonly IGameLobbyService _lobbyService;

    public LobbyController(IGameLobbyService lobbyService)
    {
        _lobbyService = lobbyService;
    }

    [HttpPost("create")]
    public ActionResult<GameLobby> CreateLobby([FromBody] CreateLobbyRequest request)
    {
        var lobby = _lobbyService.CreateLobby(request);
        return Ok(lobby);
    }

    [HttpGet("{lobbyId}")]
    public ActionResult<GameLobby> GetLobby(int lobbyId)
    {
        var lobby = _lobbyService.GetLobby(lobbyId);
        if (lobby == null)
            return NotFound();

        return Ok(lobby);
    }

    [HttpGet("lobbys")]
    public ActionResult<GameLobby> GetLobbys()
    {
        var lobbys = _lobbyService.GetAllLobbies();
        if (lobbys == null)
            return NotFound();

        return Ok(lobbys);
    }
}
