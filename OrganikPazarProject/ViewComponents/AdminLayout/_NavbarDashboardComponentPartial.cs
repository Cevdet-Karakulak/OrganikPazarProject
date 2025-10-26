using Microsoft.AspNetCore.Mvc;

namespace OrganikPazar.ViewComponents
{
    public class _NavbarDashboardComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }

}
