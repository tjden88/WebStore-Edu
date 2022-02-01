using Microsoft.AspNetCore.Mvc;

namespace WebStore_Edu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CatalogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
