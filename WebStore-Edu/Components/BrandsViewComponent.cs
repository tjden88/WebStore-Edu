using Microsoft.AspNetCore.Mvc;

namespace WebStore_Edu.Components;

public class BrandsViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}