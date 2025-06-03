using Microsoft.AspNetCore.Mvc;

namespace TelegramRPS.Server.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProfileController: ControllerBase
{
    private readonly IWebHostEnvironment _env;

    public ProfileController(IWebHostEnvironment env)
    {
        _env = env;
    }

    [HttpPost("upload-avatar")]
    public async Task<IActionResult> UploadAvatar([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Файл не загружен.");

        var allowedTypes = new[] { "image/jpeg", "image/png", "image/webp" };
        if (!allowedTypes.Contains(file.ContentType))
            return BadRequest("Недопустимый тип файла.");

        if (file.Length > 5 * 1024 * 1024)
            return BadRequest("Файл превышает допустимый размер (5MB).");

        var uploadsPath = Path.Combine(_env.WebRootPath, "uploads", "avatars");
        Directory.CreateDirectory(uploadsPath);

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var fullPath = Path.Combine(uploadsPath, fileName);

        await using (var stream = new FileStream(fullPath, FileMode.Create))
            await file.CopyToAsync(stream);

        var publicUrl = $"/uploads/avatars/{fileName}";

        return Ok(new { url = publicUrl });
    }
}