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

        public IEnumerable<Section> GetSections()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Brand> GetBrands()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetProducts(ProductFilter? filter = null)
        {
            throw new NotImplementedException();
        }

        public Product? GetProduct(int Id)
        {
            throw new NotImplementedException();
        }

        public bool Remove(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
