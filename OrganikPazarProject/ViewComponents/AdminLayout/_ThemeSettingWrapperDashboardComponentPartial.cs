using Microsoft.AspNetCore.Mvc;

namespace StoreFlow.ViewComponents
{
    public class _ThemeSettingWrapperDashboardComponentPartial: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
