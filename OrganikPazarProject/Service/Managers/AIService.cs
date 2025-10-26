using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using OrganikPazar.Service.Interfaces;

namespace OrganikPazar.Service.Managers
{
    public class AIService : IAIService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public AIService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["GeminiApiKey"];
        }

        public async Task<string> GetGeminiResponseAsync(string prompt)
        {
            var requestBody = new
            {
                contents = new[]
                {
                    new {
                        role = "user",
                        parts = new[] {
                            new { text = $"Organik Pazar’ın akıllı asistanısın 🍏. Türkçe yanıt ver ve gerektiğinde tarif, fiyat veya ürün önerisi yap. Kullanıcı mesajı: {prompt}" }
                        }
                    }
                }
            };

            var requestJson = JsonSerializer.Serialize(requestBody);
            var request = new HttpRequestMessage(
                HttpMethod.Post,
                $"https://generativelanguage.googleapis.com/v1/models/gemini-2.5-flash:generateContent?key={_apiKey}"
            );

            request.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");
            var response = await _httpClient.SendAsync(request);

            var responseText = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return $"⚠️ API hatası: {response.StatusCode} - {responseText}";

            try
            {
                using var json = JsonDocument.Parse(responseText);
                return json.RootElement
                    .GetProperty("candidates")[0]
                    .GetProperty("content")
                    .GetProperty("parts")[0]
                    .GetProperty("text")
                    .GetString();
            }
            catch
            {
                return "Üzgünüm, yanıt alınamadı. Lütfen tekrar deneyin.";
            }
        }
    }
}
