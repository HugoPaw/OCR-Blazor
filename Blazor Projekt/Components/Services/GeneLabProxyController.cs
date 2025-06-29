using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

[Route("api/[controller]")]
[ApiController]
public class GeneLabProxyController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    public GeneLabProxyController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGeneLabMeta(string id)
    {
        var client = _httpClientFactory.CreateClient("NASA");

        var url = $"https://genelab-data.ndc.nasa.gov/genelab/data/GLDS/GLDS-{id}/metadata.json";
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        try
        {
            var response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, $"NASA-Fehler: {response.StatusCode}");

            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Proxy-Fehler: {ex.Message}");
        }
    }
}
