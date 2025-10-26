using Microsoft.AspNetCore.Mvc;

namespace OrganikPazar.ViewComponents.LayoutComponents
{
    public class _ChatBotComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {            
            return View();
        }
    }
}
