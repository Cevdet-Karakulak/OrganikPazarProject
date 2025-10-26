using System.Threading.Tasks;

namespace OrganikPazar.Service.Interfaces
{
    public interface IAIService
    {
        Task<string> GetGeminiResponseAsync(string prompt);
    }
}
