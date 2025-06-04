using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class NasaController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly HttpClient _httpClient;

    public NasaController(IConfiguration config, IHttpClientFactory factory)
    {
        _config = config;
        _httpClient = factory.CreateClient();
    }

    [HttpGet("apod")]
    public async Task<IActionResult> GetApod()
    {
        var apiKey = _config["NASA_API_KEY"];
        var url = $"https://api.nasa.gov/planetary/apod?api_key={apiKey}";

        try
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Fehler beim Abrufen der NASA-Daten: {ex.Message}");
        }
    }
}
