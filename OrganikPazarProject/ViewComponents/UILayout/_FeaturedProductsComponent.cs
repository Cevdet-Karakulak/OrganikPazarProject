using Microsoft.AspNetCore.Mvc;
using OrganikPazar.Context;
using System.Linq;
using System.Collections.Generic;
using OrganikPazar.Entities;

namespace OrganikPazar.ViewComponents
{
    public class _FeaturedProductsComponent : ViewComponent
    {
        private readonly OrganikPazarContext _context;

        public _FeaturedProductsComponent(OrganikPazarContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var featuredProductsRaw = _context.Products
                .Where(p => p.Isfeatured == true)
                .OrderByDescending(p => p.Rating)
                .ToList();

            var categoryTopProducts = featuredProductsRaw
                .GroupBy(p => p.Categoryid)
                .SelectMany(g => g.Take(4))
                .ToList();

            var top8Products = featuredProductsRaw
                .OrderByDescending(p => p.Rating)
                .Take(8)
                .ToList();

            ViewBag.Top8Products = top8Products;

            return View(categoryTopProducts);
        }
    }
}
