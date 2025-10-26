using Microsoft.AspNetCore.Mvc;

namespace OrganikPazar.ViewComponents
{
    public class _HeaderComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
