using System.Threading.Tasks;

namespace OrganikPazar.Service.Interfaces
{
    public interface INaturalQueryService
    {
        Task<string> ProcessAsync(string query);
    }
}
