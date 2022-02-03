using System.Net.Http.Json;
using MapsterMapper;
using WebStore_Edu.Domain;
using WebStore_Edu.Domain.DTO;
using WebStore_Edu.Domain.Entityes;
using WebStore_Edu.Interfaces.Services;
using WebStore_Edu.WebAPI.Clients.Base;

namespace WebStore_Edu.WebAPI.Clients.Products
{
    public class ProductsClient : BaseClient, IProductData
    {
        private readonly IMapper _Mapper;

        public ProductsClient(HttpClient HttpHttp, IMapper Mapper) : base(HttpHttp, "api/products")
        {
            _Mapper = Mapper;
        }

        public IEnumerable<Section> GetSections()
        {
            var sections = Get<IEnumerable<SectionDTO>>($"{Address}/sections") ?? Enumerable.Empty<SectionDTO>();
            return _Mapper.Map<IEnumerable<Section>>(sections);
        }

        public IEnumerable<Brand> GetBrands()
        {
            var brands = Get<IEnumerable<BrandDTO>>($"{Address}/brands") ?? Enumerable.Empty<BrandDTO>();
            return _Mapper.Map<IEnumerable<Brand>>(brands);
        }

        public IEnumerable<Product> GetProducts(ProductFilter? filter = null)
        {
            var products = Post(Address, filter ?? new())
                                 .Content
                                 .ReadFromJsonAsync<IEnumerable<ProductDTO>>()
                                 .Result
                             ?? Enumerable.Empty<ProductDTO>();

            return _Mapper.Map<IEnumerable<Product>>(products);
        }

        public Product? GetProduct(int Id)
        {
            var product = Get<ProductDTO?>($"{Address}/{Id}");
            return product is null
                ? null
                : _Mapper.Map<Product>(product);
        }

        public bool Remove(int Id) => Delete($"{Address}/{Id}").IsSuccessStatusCode;
    }
}
