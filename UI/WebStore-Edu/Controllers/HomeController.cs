using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using WebStore_Edu.Services.Interfaces;
using WebStore_Edu.ViewModels;

namespace WebStore_Edu.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index([FromServices] IProductData ProductData, [FromServices] IMapper Mapper) 
        {
            var featuredProducts = ProductData.GetProducts().Take(6);
            ViewBag.FeaturedProducts = Mapper.Map<IEnumerable<ProductViewModel>>(featuredProducts);
            return View();
            //return View("Index"); // Если названия в папке Views не совпадают
            // return View("~/Views/Home/Index.cshtml"); // Полный путь
        }

        public IActionResult NotFoundPage() => View();

        [Route("Error/{Code}")]
        public IActionResult Error(int Code)
        {
            if (Code == 404)
            {
                return View("NotFoundPage");
            }

            return StatusCode(Code);
        }

        public IActionResult Contacts() => View();
    }
}
