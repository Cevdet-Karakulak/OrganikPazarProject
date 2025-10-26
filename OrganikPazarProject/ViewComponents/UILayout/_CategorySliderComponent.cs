using Microsoft.AspNetCore.Mvc;
using OrganikPazar.Context;
using System.Linq;

namespace OrganikPazar.ViewComponents
{
    public class _CategorySliderComponent : ViewComponent
    {
        private readonly OrganikPazarContext _context;

        public _CategorySliderComponent(OrganikPazarContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _context.Categories
                .OrderBy(c => c.Categoryname)
                .Take(10) // ilk 10 kategori slider’da gösterilir
                .ToList();

            return View(categories);
        }
    }
}
