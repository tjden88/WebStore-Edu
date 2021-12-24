using Microsoft.AspNetCore.Mvc;

namespace WebStore_Edu.Components
{
    public class SectionsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
