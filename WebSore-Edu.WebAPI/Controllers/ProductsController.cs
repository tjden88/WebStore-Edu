using Microsoft.AspNetCore.Mvc;
using WebStore_Edu.Domain;
using WebStore_Edu.Interfaces.Services;

namespace WebSore_Edu.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductData _ProductData;

        public ProductsController(IProductData ProductData) => _ProductData = ProductData;

        [HttpGet("sections")]
        public IActionResult GetSections() => Ok(_ProductData.GetSections());


        [HttpGet("brands")]
        public IActionResult GetBrands() => Ok(_ProductData.GetBrands());

        [HttpPost]
        public IActionResult GetProducts(ProductFilter? Filter = null) => Ok(_ProductData.GetProducts(Filter));


        [HttpGet("{Id}")]
        public IActionResult GetProduct(int Id)
        {
            var product = _ProductData.GetProduct(Id);
            return product is null
                ? NotFound()
                : Ok(product);
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var success = _ProductData.Remove(Id);
            return success ? Ok(success) : NotFound(success);
        }
    }
}
