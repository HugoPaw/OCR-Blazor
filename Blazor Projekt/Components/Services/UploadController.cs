using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

[ApiController]
[Route("api/[controller]")]
public class UploadController : ControllerBase
{
    private readonly IWebHostEnvironment _env;

    public UploadController(IWebHostEnvironment env)
    {
        _env = env;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromForm] IFormFile file, [FromForm] string type)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Keine Datei.");

        var uploads = Path.Combine(_env.ContentRootPath, "Uploads");
        Directory.CreateDirectory(uploads);
        var filePath = Path.Combine(uploads, file.FileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        if (type == "mp3")
        {
            // ffmpeg aufrufen
            var mp3Path = Path.ChangeExtension(filePath, ".mp3");
            var process = Process.Start(new ProcessStartInfo
            {
                FileName = "ffmpeg",
                Arguments = $"-i \"{filePath}\" \"{mp3Path}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            });
            process.WaitForExit();
        }
        else if (type == "text")
        {
            // z.B. Whisper aufrufen oder eine Speech-to-Text-API verwenden
        }

        return Ok();
    }
}
