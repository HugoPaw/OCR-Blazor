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
            return BadRequest("Keine Datei empfangen.");

        // Zielordner erstellen
        var uploadsPath = Path.Combine(_env.ContentRootPath, "Uploads");
        Directory.CreateDirectory(uploadsPath);
        var inputPath = Path.Combine(uploadsPath, file.FileName);

        // Datei speichern
        using (var stream = new FileStream(inputPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        if (type == "mp3")
        {
            var mp3Path = Path.ChangeExtension(inputPath, ".mp3");

            var startInfo = new ProcessStartInfo
            {
                FileName = "ffmpeg",
                Arguments = $"-i \"{inputPath}\" \"{mp3Path}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            try
            {
                var process = Process.Start(startInfo);
                string output = await process.StandardError.ReadToEndAsync(); // FFMPEG schreibt in stderr
                process.WaitForExit();

                if (process.ExitCode != 0)
                    return StatusCode(500, $"FFMPEG-Fehler: {output}");

                return Ok(new { message = "MP3 erfolgreich erstellt.", file = Path.GetFileName(mp3Path) });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Fehler beim Konvertieren: {ex.Message}");
            }
        }
        else if (type == "text")
        {
            // 🧪 Platzhalter-Transkription – später durch echte Speech-to-Text-Logik ersetzen
            string dummyTranscription = $"[Mock-Transkript] Transkription von Datei '{file.FileName}' erfolgreich simuliert.";

            return Ok(new
            {
                message = "Transkription abgeschlossen.",
                text = dummyTranscription
            });
        }

        return BadRequest("Unbekannter Typ.");
    }
}
