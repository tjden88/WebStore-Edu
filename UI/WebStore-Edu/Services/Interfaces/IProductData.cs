using WebStore_Edu.Domain;
using WebStore_Edu.Domain.Entityes;

namespace WebStore_Edu.Services.Interfaces
{
    public interface IProductData
    {
        /// <summary> Получить все категории товаров </summary>
        IEnumerable<Section> GetSections();

        /// <summary> Получить все бренды </summary>
        IEnumerable<Brand> GetBrands();

        /// <summary> Получить данные товаров с фильтром </summary>
        IEnumerable<Product> GetProducts(ProductFilter? filter = null);

        Product? GetProduct(int Id);

        bool Remove(int Id);

    }
}
