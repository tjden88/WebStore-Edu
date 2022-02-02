namespace WebStore_Edu.ViewModels
{
    public class CartViewModel
    {
        public IEnumerable<(ProductViewModel Product, int Quantity)> Items { get; set; }

        public decimal TotalPrice => Items.Sum(i => i.Product.Price * i.Quantity);
    }
}
