using Microsoft.AspNetCore.SignalR;
using OrganikPazar.Service.Interfaces;
using System.Threading.Tasks;

namespace OrganikPazar.Hubs
{
    public class ChatHub : Hub
    {
        private readonly INaturalQueryService _queryService;
        private readonly IAIService _aiService;

        public ChatHub(INaturalQueryService queryService, IAIService aiService)
        {
            _queryService = queryService;
            _aiService = aiService;
        }

        public async Task SendMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                await Clients.Caller.SendAsync("ReceiveMessage", "⚠️ Boş mesaj gönderilemez.");
                return;
            }

            string response = await _queryService.ProcessAsync(message);

            if (string.IsNullOrEmpty(response))
            {
                response = await _aiService.GetGeminiResponseAsync(
                    $"Organik Pazar bağlamında bu mesaja Türkçe yanıt ver: {message}");
            }

            await Clients.Caller.SendAsync("ReceiveMessage",
                string.IsNullOrWhiteSpace(response)
                ? "⚠️ Şu anda yanıt veremiyorum. Lütfen tekrar deneyin."
                : response);
        }
    }
}
