using System.Collections.Generic;
using System.Linq;

namespace WebStore_Edu.Domain.Entityes
{
    public class Cart
    {
        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();

        public int TotalCount => Items.Sum(i => i.Quantity);
    }
}
