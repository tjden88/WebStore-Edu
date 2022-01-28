using Microsoft.AspNetCore.Mvc;

namespace WebStore_Edu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}
