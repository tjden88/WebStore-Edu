using System.Collections.Generic;
using System.Linq;

namespace WebStore_Edu.Domain.ViewModels
{
    public class CartViewModel
    {
        public IEnumerable<(ProductViewModel Product, int Quantity)> Items { get; set; }

        public decimal TotalPrice => Items.Sum(i => i.Product.Price * i.Quantity);
    }
}
