using Microsoft.AspNetCore.Mvc;
using WebStore_Edu.Domain.ViewModels;

namespace WebStore_Edu.Components
{
    public class BreadCrumbsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var model = new BreadCrumbsViewModel();

            return View(model);
        }
    }
}
