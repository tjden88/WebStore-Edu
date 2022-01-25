
namespace WebStore_Edu.ViewModels
{
    public class ShopViewModel
    {
        public int? BrandId { get; set; }

        public int? SectionId { get; set; }

        IEnumerable<ProductViewModel> Products { get; set; } = Enumerable.Empty<ProductViewModel>();
    }
}
