using Microsoft.AspNetCore.Mvc;
using OrganikPazar.Context;
using OrganikPazar.Helpers;
using System.Linq;

namespace OrganikPazar.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly OrganikPazarContext _context;

        public StatisticsController(OrganikPazarContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var productCount = _context.Products.Count();
            ViewBag.TotalProductCount = FormatHelper.FormatNumber(productCount);

            var totalSales = _context.Orders.Sum(o => o.Totalprice);
            ViewBag.TotalSales = FormatHelper.FormatCurrency(totalSales);

            var deliveredOrders = _context.Orders.Count(o => o.Status == "Teslim Edildi");
            ViewBag.DeliveredOrders = FormatHelper.FormatNumber(deliveredOrders);

            var avgOrder = _context.Orders.Average(o => o.Totalprice);
            ViewBag.AvgOrderPrice = FormatHelper.FormatCurrency((decimal)avgOrder);

            var bestProduct = _context.Products.OrderByDescending(p => p.Rating).FirstOrDefault();
            ViewBag.BestProductName = bestProduct?.Productname ?? "-";
            ViewBag.BestProductRating = bestProduct?.Rating?.ToString("0.0") ?? "0.0";

            var topStock = _context.Products.OrderByDescending(p => p.Stock).FirstOrDefault();
            ViewBag.TopStockProduct = topStock?.Productname ?? "-";
            ViewBag.TopStockCount = FormatHelper.FormatNumber(topStock?.Stock ?? 0);

            ViewBag.TopCity = _context.Orders
                .GroupBy(o => o.City)
                .Select(g => new { City = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Select(x => x.City)
                .FirstOrDefault() ?? "-";

            var customerCount = _context.Customers.Count();
            ViewBag.CustomerCount = FormatHelper.FormatNumber(customerCount);

            ViewBag.TopCategory = _context.Orders
                .Join(_context.Products, o => o.Productid, p => p.Productid, (o, p) => new { o, p })
                .Join(_context.Categories, op => op.p.Categoryid, c => c.Categoryid, (op, c) => new { op.o, op.p, c })
                .GroupBy(x => x.c.Categoryname)
                .Select(g => new { Category = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Select(x => x.Category)
                .FirstOrDefault() ?? "-";

            return View();
        }
    }
}
