using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using WebStore_Edu.Domain;
using WebStore_Edu.Domain.ViewModels;
using WebStore_Edu.Interfaces.Services;

namespace WebStore_Edu.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductData _ProductData;
        private readonly ILogger<ShopController> _Logger;
        private readonly IMapper _Mapper;

        public ShopController(IProductData ProductData, ILogger<ShopController> Logger, IMapper Mapper)
        {
            _ProductData = ProductData;
            _Logger = Logger;
            _Mapper = Mapper;
        }

        public IActionResult Index(int? SectionId, int? BrandId)
        {
            var filter = new ProductFilter()
            {
                BrandId = BrandId,
                SectionId = SectionId
            };

            var products = _ProductData
                .GetProducts(filter)
                .OrderBy(p=>p.Order);

            var model = new ShopViewModel()
            {
                BrandId = BrandId,
                SectionId = SectionId,
                Products = _Mapper.Map<IEnumerable<ProductViewModel>>(products)
            };

            return View(model);
        }

        public IActionResult ProductDetails(int Id)
        {
            var product = _ProductData.GetProduct(Id);
            if (product is null)
                return NotFound();

            return View(_Mapper.Map<ProductViewModel>(product));
        }

    }
}
