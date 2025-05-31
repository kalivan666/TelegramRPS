using Microsoft.AspNetCore.Mvc;
using TelegramRPS.Server.Games.DurakGame;
using TelegramRPS.Server.Interfaces;

namespace TelegramRPS.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameController : ControllerBase
{
    private readonly IGameService _gameService;

    private bool IsRequestFromClient()
    {
        var origin = Request.Headers["Origin"].ToString();
        return origin == "https://telegramrps.ru";
    }

    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpPost("create-from-telegram")]
    public ActionResult<DurakGame> CreateFromTelegram([FromBody] Guid userProfileId)
    {
        if (!IsRequestFromClient())
            return Forbid();

        var game = _gameService.CreateGameFromTelegram(userProfileId);
        return Ok(game);
    }

    [HttpGet("status")]
    public IActionResult GetStatus()
    {
        return Ok(new {status = "Server is running"});
    }
}