using Microsoft.AspNetCore.Mvc;

namespace OrganikPazar.ViewComponents
{
    public class _FooterComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
