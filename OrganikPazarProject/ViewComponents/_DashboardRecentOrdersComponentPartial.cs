using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganikPazar.Context;
using OrganikPazar.Entities;

namespace OrganikPazar.ViewComponents
{
    public class _DashboardRecentOrdersComponentPartial : ViewComponent
    {
        private readonly OrganikPazarContext _context;

        public _DashboardRecentOrdersComponentPartial(OrganikPazarContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Product)
                .OrderByDescending(o => o.Orderdate)  
                .Take(10)                             
                .ToList();

            return View(values);
        }
    }
}
