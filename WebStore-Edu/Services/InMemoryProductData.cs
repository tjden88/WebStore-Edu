using WebStore_Edu.Data;
using WebStore_Edu.Domain.Entityes;
using WebStore_Edu.Services.Interfaces;

namespace WebStore_Edu.Services;

public class InMemoryProductData : IProductData
{
    public IEnumerable<Section> GetSections() => TestData.Sections;

    public IEnumerable<Brand> GetBrands() => TestData.Brands;
}