using System.Net.Http.Json;
using System.Text.Json;
using SmartBlog.Application.Interfaces;

namespace SmartBlog.Infrastructure.Services;

// Implementation av IOpenAiService - hanterar kommunikation med OpenAI:s API
public class OpenAiService : IOpenAiService
{
    // HttpClient som är konfigurerad med basadress och API-nyckel i DependencyInjection
    private readonly HttpClient _httpClient;

    public OpenAiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GenerateSummaryAsync(string content, CancellationToken cancellationToken = default)
    {
        // Bygg upp request-objektet som OpenAI:s Chat Completions API förväntar sig
        var requestBody = new
        {
            model = "gpt-4o-mini",
            messages = new[]
            {
                new { role = "system", content = "Du är en hjälpsam assistent som sammanfattar blogginlägg på svenska. Svara med en kort och koncis sammanfattning." },
                new { role = "user", content = $"Sammanfatta följande blogginlägg:\n\n{content}" }
            },
            max_tokens = 300
        };

        // Skicka POST-anrop till OpenAI:s chat/completions-endpoint
        var response = await _httpClient.PostAsJsonAsync("chat/completions", requestBody, cancellationToken);

        // Kasta exception om anropet misslyckades (fångas av ErrorHandlingMiddleware)
        response.EnsureSuccessStatusCode();

        // Läs svaret som JSON
        var json = await response.Content.ReadFromJsonAsync<JsonElement>(cancellationToken);

        // Plocka ut textsvaret från OpenAI:s JSON-struktur: choices[0].message.content
        var summary = json
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString();

        return summary ?? "Kunde inte generera en sammanfattning.";
    }
}
