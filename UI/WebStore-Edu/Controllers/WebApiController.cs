using Microsoft.AspNetCore.Mvc;
using WebStore_Edu.Interfaces.TestApi;

namespace WebStore_Edu.Controllers
{
    public class WebApiController : Controller
    {
        private readonly IValuesApiService _ValuesApiService;

        public WebApiController(IValuesApiService ValuesApiService) => _ValuesApiService = ValuesApiService;

        public IActionResult Index()
        {
            var values = _ValuesApiService.GetAll();
            return View(values);
        }
    }
}
