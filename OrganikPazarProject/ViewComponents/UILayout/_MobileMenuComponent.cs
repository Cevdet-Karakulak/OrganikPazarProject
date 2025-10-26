using Microsoft.AspNetCore.Mvc;

namespace OrganikPazar.ViewComponents
{
    public class _MobileMenuComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
