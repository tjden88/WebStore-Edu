using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using WebStore_Edu.Services.Interfaces;
using WebStore_Edu.ViewModels;

namespace WebStore_Edu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CatalogController : Controller
    {
        private readonly IProductData _ProductData;
        private readonly IMapper _Mapper;

        public CatalogController(IProductData ProductData, IMapper Mapper)
        {
            _ProductData = ProductData;
            _Mapper = Mapper;
        }
        public IActionResult Index()
        {
            var products = _ProductData.GetProducts();
            return View(_Mapper.Map<IEnumerable<ProductViewModel>>(products));
        }
    }
}
