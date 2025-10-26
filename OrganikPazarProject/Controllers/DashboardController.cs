using System;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganikPazar.Context;
using OrganikPazar.Helpers;

namespace OrganikPazar.Controllers
{
    public class DashboardController : Controller
    {
        private readonly OrganikPazarContext _context;

        public DashboardController(OrganikPazarContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var now = DateTime.Now;

            var startThisMonth = new DateTime(now.Year, now.Month, 1);
            var startPrevMonth = startThisMonth.AddMonths(-1);
            var endPrevMonth = startThisMonth.AddDays(-1);

            var totalPrevCustomers = _context.Customers.Count(c => c.Registerdate >= startPrevMonth && c.Registerdate <= endPrevMonth);
            var totalThisMonthCustomers = _context.Customers.Count(c => c.Registerdate >= startThisMonth);
            ViewBag.customerGrowth = CalculateChange(totalPrevCustomers, totalThisMonthCustomers);

            var currentProductCount = _context.Products.Count();
            var prevProductCount = currentProductCount - 5; 
            ViewBag.productChange = CalculateChange(prevProductCount, currentProductCount);

            var prevOrders = _context.Orders.Count(o => o.Orderdate >= startPrevMonth && o.Orderdate <= endPrevMonth);
            var currentOrders = _context.Orders.Count(o => o.Orderdate >= startThisMonth);
            ViewBag.orderChange = CalculateChange(prevOrders, currentOrders);

            var prevSales = _context.Orders
                .Where(o => o.Orderdate >= startPrevMonth && o.Orderdate <= endPrevMonth)
                .Sum(o => (decimal?)o.Totalprice) ?? 0;
            var currentSales = _context.Orders
                .Where(o => o.Orderdate >= startThisMonth)
                .Sum(o => (decimal?)o.Totalprice) ?? 0;
            ViewBag.salesChange = CalculateChange(prevSales, currentSales);

            var avgBalancePrev = _context.Customers
                .Where(c => c.Registerdate <= endPrevMonth)
                .Average(c => (decimal?)c.CustomerBalance) ?? 0;
            var avgBalanceNow = _context.Customers
                .Average(c => (decimal?)c.CustomerBalance) ?? 0;
            ViewBag.balanceChange = CalculateChange(avgBalancePrev, avgBalanceNow);

            ViewBag.totalCustomerCount = _context.Customers.Count();
            ViewBag.totalProductCount = _context.Products.Count();
            ViewBag.totalCategoryCount = _context.Categories.Count();
            ViewBag.orderCount = FormatHelper.FormatNumber(_context.Orders.Count());
            ViewBag.avgCustomerBalance = FormatHelper.FormatCurrency(avgBalanceNow);
            ViewBag.totalSales = FormatHelper.FormatCurrency(currentSales);

            return View();
        }

        private string CalculateChange(decimal oldValue, decimal newValue)
        {
            if (oldValue == 0 && newValue == 0)
                return "%0 değişim";

            if (oldValue == 0 && newValue > 0)
                return "+%100 artış";

            var change = ((newValue - oldValue) / oldValue) * 100;
            change = Math.Clamp(change, -100, 100);

            var formatted = Math.Round(Math.Abs(change), 1, MidpointRounding.AwayFromZero);

            if (change > 0)
                return $"+%{formatted} artış";
            else if (change < 0)
                return $"-%{formatted} azalış";
            else
                return "%0 değişim";
        }
    }
}
