using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganikPazar.Context;

namespace OrganikPazar.ViewComponents.DashboardComponents
{
    public class _Last5ProductsDashboardComponentPartial : ViewComponent
    {
        private readonly OrganikPazarContext _context;

        public _Last5ProductsDashboardComponentPartial(OrganikPazarContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _context.Products
                .OrderByDescending(x => x.Productid)
                .Take(5)
                .AsNoTracking()
                .ToListAsync();

            return View(values);
        }
    }
}
