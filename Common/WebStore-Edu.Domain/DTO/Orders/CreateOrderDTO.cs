using System.Collections.Generic;
using WebStore_Edu.Domain.ViewModels;

namespace WebStore_Edu.Domain.DTO.Orders
{
    public class CreateOrderDTO
    {
        public IEnumerable<OrderItemDTO> CartItems { get; set; }

        public OrderVievModel OrderModel { get; set; }
    }
}
