using WebStore_Edu.Domain.ViewModels;

namespace WebStore_Edu.Domain.DTO
{
    public class CreateOrderDTO
    {
        public CartViewModel Cart { get; set; }

        public OrderVievModel OrderModel { get; set; }
    }
}
