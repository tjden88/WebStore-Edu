using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore_Edu.Domain.Identity;
using WebStore_Edu.Domain.ViewModels;
using WebStore_Edu.Services.Interfaces;

namespace WebStore_Edu.Areas.Admin.Controllers
{
    [Authorize(Roles = Role.Administrators), Area("Admin")]
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

        public IActionResult Edit(int Id)
        {
            var product = _ProductData.GetProduct(Id);
            if (product is null)
                return NotFound();

            return View(_Mapper.Map<ProductViewModel>(product));
        }

        [HttpPost]
        public IActionResult Edit(ProductViewModel model)
        {
            // Изменить товар в сервисе TODO: не доработано

            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var removed = _ProductData.Remove(Id);
            if (!removed)
                return NotFound();
            return RedirectToAction("Index");
        }
    }
}
