using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganikPazar.Context;

namespace OrganikPazar.ViewComponents.DashboardComponents
{
    public class _DashboardLastLogsComponentPartial : ViewComponent
    {
        private readonly OrganikPazarContext _context;

        public _DashboardLastLogsComponentPartial(OrganikPazarContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var lastLogs = await _context.Logs
                .OrderByDescending(x => x.Actiondate)
                .Take(5)
                .AsNoTracking()
                .ToListAsync();

            return View(lastLogs);
        }
    }
}
