using Microsoft.AspNetCore.Mvc;
using OrganikPazar.Context;
using OrganikPazar.Models.ViewModels;
using System.Linq;

namespace OrganikPazar.ViewComponents
{
    public class _DashboardOrderStatusChartComponentPartial : ViewComponent
    {
        private readonly OrganikPazarContext _context;

        public _DashboardOrderStatusChartComponentPartial(OrganikPazarContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var result = _context.Orders
                .GroupBy(o => o.Status)
                .Select(g => new OrderStatusChartViewModel
                {
                    Status = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .ToList();

            return View(result);
        }
    }
}
