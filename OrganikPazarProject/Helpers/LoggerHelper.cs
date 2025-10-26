using Microsoft.AspNetCore.Http;
using OrganikPazar.Context;
using OrganikPazar.Entities;

namespace OrganikPazar.Helpers
{
    public static class LoggerHelper
    {
        public static async Task LogAsync(
            OrganikPazarContext context,
            IHttpContextAccessor httpContextAccessor,
            string actionType,
            string entity,
            string? detail = null)
        {
            var ip = httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Bilinmiyor";
            var username = httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "Anonim";

            context.Logs.Add(new Log
            {
                Username = username,
                Actiontype = actionType,
                Entity = entity,
                IpAddress = ip,
                ActionDetail = detail, 
                Actiondate = DateTime.Now
            });

            await context.SaveChangesAsync();
        }
    }
}
