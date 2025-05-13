using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorProjekt.Services
{
    public class AiService
    {
        private readonly HttpClient _http;

        public AiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<string> GetTipAsync(string query)
        {
            try
            {
                var url = $"https://api.duckduckgo.com/?q={Uri.EscapeDataString(query)}&format=json&no_redirect=1";
                var response = await _http.GetFromJsonAsync<DuckDuckGoResult>(url);
                return !string.IsNullOrWhiteSpace(response?.AbstractText)
                    ? response.AbstractText
                    : "Kein Tipp gefunden.";
            }
            catch
            {
                return "Fehler beim Abrufen des Tipps.";
            }
        }

        private class DuckDuckGoResult
        {
            [JsonPropertyName("AbstractText")]
            public string AbstractText { get; set; } = "";
        }
    }
}
