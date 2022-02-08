using Microsoft.AspNetCore.Mvc;
using SimpleMvcSitemap;
using WebStore_Edu.Interfaces.Services;

namespace WebStore_Edu.Controllers
{
    public class SitemapController : ControllerBase
    {
        public IActionResult Index([FromServices] IProductData ProductData)
        {
            var nodes = new List<SitemapNode>()
            {
                new(Url.Action("Index", "Home")),
                new(Url.Action("Contacts", "Home")),
                new(Url.Action("Index", "Blogs")),
                new(Url.Action("Blog", "Blogs")),
                new(Url.Action("Index", "WebApi")),
                new(Url.Action("Index", "Shop")),
            };

            nodes.AddRange(ProductData.GetSections().Select(s => new SitemapNode(Url.Action("Index", "Shop", new { SectionId = s.Id}))));
            nodes.AddRange(ProductData.GetBrands().Select(b => new SitemapNode(Url.Action("Index", "Shop", new { BrandId = b.Id}))));
            nodes.AddRange(ProductData.GetProducts().Select(p => new SitemapNode(Url.Action("ProductDetails", "Shop", new {p.Id}))));

            return new SitemapProvider().CreateSitemap(new SitemapModel(nodes));
        }
    }
}
