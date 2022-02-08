using Microsoft.AspNetCore.Mvc;
using WebStore_Edu.Domain.ViewModels;
using WebStore_Edu.Interfaces.Services;

namespace WebStore_Edu.Components
{
    public class BreadCrumbsViewComponent : ViewComponent
    {
        private readonly IProductData _ProductData;

        public BreadCrumbsViewComponent(IProductData ProductData)
        {
            _ProductData = ProductData;
        }
        public IViewComponentResult Invoke(string? ProductName = null)
        {
            var model = new BreadCrumbsViewModel();

            var sectionId = int.TryParse(HttpContext.Request.Query["SectionId"], out var sId) ? sId: (int?)null;
            var brandId = int.TryParse(HttpContext.Request.Query["BrandId"], out var bId) ? bId: (int?)null;


            var allSections = _ProductData.GetSections().ToArray();
            model.Section = allSections.FirstOrDefault(s => s.Id == sectionId);
            if (model.Section is {ParentId: {} parentId})
            {
                model.ParentSection = allSections.FirstOrDefault(s => s.Id == parentId);
            }
            model.Brand = _ProductData.GetBrands().FirstOrDefault(b => b.Id == brandId);
            model.ProductName = ProductName;

            return View(model);
        }
    }
}
