using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using WebStore_Edu.Domain;
using WebStore_Edu.Services.Interfaces;
using WebStore_Edu.ViewModels;

namespace WebStore_Edu.Components;

public class BrandsViewComponent : ViewComponent
{
    private readonly IMapper _Mapper;
    private readonly IProductData _ProductData;

    public BrandsViewComponent(IMapper Mapper, IProductData ProductData)
    {
        _Mapper = Mapper;
        _ProductData = ProductData;
    }


    public IViewComponentResult Invoke()
    {
        var brands = _ProductData.GetBrands();

        var brandsVm = _Mapper.Map<IEnumerable<BrandViewModel>>(brands)
            .ToArray()
            .OrderBy(b => b.Order);

        foreach (var brandViewModel in brandsVm)
        {
            var prodCount = _ProductData
                .GetProducts(new ProductFilter()
                {
                    BrandId = brandViewModel.Id
                })
                .Count();

            brandViewModel.ProductsCount = prodCount;
        }

        return View(brandsVm);
    }
}