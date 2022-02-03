using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using WebStore_Edu.Domain;
using WebStore_Edu.Domain.DTO;
using WebStore_Edu.Interfaces.Services;

namespace WebSore_Edu.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductData _ProductData;
        private readonly IMapper _Mapper;

        public ProductsController(IProductData ProductData, IMapper Mapper)
        {
            _ProductData = ProductData;
            _Mapper = Mapper;
        }

        [HttpGet("sections")]
        public IActionResult GetSections() => Ok(_Mapper.Map<IEnumerable<SectionDTO>>(_ProductData.GetSections()));


        [HttpGet("brands")]
        public IActionResult GetBrands() => Ok(_Mapper.Map<IEnumerable<BrandDTO>>(_ProductData.GetBrands()));

        [HttpPost]
        public IActionResult GetProducts(ProductFilter? Filter = null) => Ok(_Mapper.Map<IEnumerable<ProductDTO>>(_ProductData.GetProducts(Filter)));


        [HttpGet("{Id}")]
        public IActionResult GetProduct(int Id)
        {
            var product = _ProductData.GetProduct(Id);
            return product is null
                ? NotFound()
                : Ok(_Mapper.Map<ProductDTO>(product));
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var success = _ProductData.Remove(Id);
            return success ? Ok(success) : NotFound(success);
        }
    }
}
