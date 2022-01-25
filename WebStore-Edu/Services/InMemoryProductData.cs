using WebStore_Edu.Data;
using WebStore_Edu.Domain;
using WebStore_Edu.Domain.Entityes;
using WebStore_Edu.Services.Interfaces;

namespace WebStore_Edu.Services;

public class InMemoryProductData : IProductData
{
    public IEnumerable<Section> GetSections() => TestData.Sections;

    public IEnumerable<Brand> GetBrands() => TestData.Brands;

    public IEnumerable<Product> GetProducts(ProductFilter? filter = null)
    {
        var query = TestData.Products;
        if (filter?.SectionId is { } section)
        {
            query = query.Where(p => p.SectionId == section);
        }
        if (filter?.BrandId is { } brand)
        {
            query = query.Where(p => p.BrandId == brand);
        }

        return query;
    }
}