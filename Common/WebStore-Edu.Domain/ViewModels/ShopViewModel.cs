
using System.Collections.Generic;
using System.Linq;

namespace WebStore_Edu.Domain.ViewModels
{
    public class ShopViewModel
    {
        public int? BrandId { get; set; }

        public int? SectionId { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; } = Enumerable.Empty<ProductViewModel>();
    }
}
