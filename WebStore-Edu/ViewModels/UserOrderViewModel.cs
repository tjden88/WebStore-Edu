using WebStore_Edu.Domain.Entityes.Orders;

namespace WebStore_Edu.ViewModels
{
    public class UserOrderViewModel
    {
        public int Id { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Notes { get; set; }

        public DateTimeOffset Date { get; set; }

        public decimal TotalPrice;

        public OrderStatus OrderStatus { get; set; }

    }
}
