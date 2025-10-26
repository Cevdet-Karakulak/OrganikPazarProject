using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace OrganikPazar.ViewComponents
{
    public class _BlogPreviewComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
{
    return View();
}
    }
}
