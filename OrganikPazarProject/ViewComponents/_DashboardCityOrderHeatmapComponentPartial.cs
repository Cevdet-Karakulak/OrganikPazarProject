using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganikPazar.Context;
using OrganikPazar.Models.ViewModels;

namespace OrganikPazar.ViewComponents
{
    public class _DashboardCityOrderHeatmapComponentPartial : ViewComponent
    {
        private readonly OrganikPazarContext _context;

        public _DashboardCityOrderHeatmapComponentPartial(OrganikPazarContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var data = _context.Orders
                .Include(o => o.Product)
                .ThenInclude(p => p.Category)
                .Where(o => o.City != null)
                .GroupBy(o => o.City)
                .Select(g => new CityOrderViewModel
                {
                    City = g.Key,
                    OrderCount = g.Count(),
                    AvgPrice = g.Average(x => (decimal?)x.Totalprice) ?? 0,
                    TopCategory = g.GroupBy(x => x.Product.Category.Categoryname)
                                   .OrderByDescending(c => c.Count())
                                   .Select(c => c.Key)
                                   .FirstOrDefault()
                })
                .ToList();

            return View(data);
        }
    }
}
