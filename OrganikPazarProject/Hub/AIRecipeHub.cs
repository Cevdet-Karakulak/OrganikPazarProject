using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace OrganikPazar.Hubs
{
    public class AIRecipeHub : Hub
    {
        public async Task SendRecipe(string message)
        {
            await Clients.All.SendAsync("ReceiveRecipe", message);
        }
    }
}
