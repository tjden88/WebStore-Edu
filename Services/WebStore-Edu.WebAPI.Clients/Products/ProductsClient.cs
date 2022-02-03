using System.Net.Http.Json;
using WebStore_Edu.Domain;
using WebStore_Edu.Domain.Entityes;
using WebStore_Edu.Interfaces.Services;
using WebStore_Edu.WebAPI.Clients.Base;

namespace WebStore_Edu.WebAPI.Clients.Products
{
    public class ProductsClient : BaseClient, IProductData
    {
        public ProductsClient(HttpClient HttpHttp) : base(HttpHttp, "api/products")
        {
        }

        public IEnumerable<Section> GetSections() => Get<IEnumerable<Section>>($"{Address}/sections") ?? Enumerable.Empty<Section>();

        public IEnumerable<Brand> GetBrands() => Get<IEnumerable<Brand>>($"{Address}/brands") ?? Enumerable.Empty<Brand>();

        public IEnumerable<Product> GetProducts(ProductFilter? filter = null) =>
            Post(Address, filter ?? new())
                .Content
                .ReadFromJsonAsync<IEnumerable<Product>>()
                .Result
            ?? Enumerable.Empty<Product>();

        public Product? GetProduct(int Id) => Get<Product?>($"{Address}/{Id}");

        public bool Remove(int Id) => Delete($"{Address}/{Id}").IsSuccessStatusCode;
    }
}
