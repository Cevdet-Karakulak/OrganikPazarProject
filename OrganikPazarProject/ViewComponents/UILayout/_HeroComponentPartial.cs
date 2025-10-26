using Microsoft.AspNetCore.Mvc;
using OrganikPazar.Context; // PostgreSQL bağlamı
using System.Linq;

namespace OrganikPazar.ViewComponents
{
    public class _HeroComponentPartial : ViewComponent
    {
        private readonly OrganikPazarContext _context;

        public _HeroComponentPartial(OrganikPazarContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _context.Categories
                .OrderBy(c => c.Categoryname)
                .Take(12)
                .ToList();

            return View(categories);
        }
    }
}
