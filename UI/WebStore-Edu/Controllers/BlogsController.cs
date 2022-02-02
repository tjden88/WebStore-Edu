using Microsoft.AspNetCore.Mvc;

namespace WebStore_Edu.Controllers
{
    public class BlogsController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult Blog() => View();
    }
}
