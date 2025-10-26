using Microsoft.AspNetCore.Mvc;
using OrganikPazar.Context;
using OrganikPazar.Models.ViewModels;
using System.Linq;

namespace OrganikPazar.ViewComponents
{
    public class _DashboardOrderDateChartComponentPartial : ViewComponent
    {
        private readonly OrganikPazarContext _context;

        public _DashboardOrderDateChartComponentPartial(OrganikPazarContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var rawData = _context.Orders
                .Where(o => o.Orderdate.HasValue)
                .ToList(); 

            var data = rawData
                .GroupBy(o => o.Orderdate.Value.Date)
                .Select(g => new OrderDateViewModel
                {
                    Date = g.Key.ToString("yyyy-MM-dd"), 
                    Count = g.Count()
                })
                .OrderBy(x => x.Date)
                .Take(30)
                .ToList();

            return View(data);
        }
    }
}
