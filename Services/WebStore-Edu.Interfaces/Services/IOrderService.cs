using WebStore_Edu.Domain.Entityes.Orders;
using WebStore_Edu.Domain.Identity;
using WebStore_Edu.Domain.ViewModels;

namespace WebStore_Edu.Interfaces.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetUserOrdersAsync(User User, CancellationToken Cancel = default);

        Task<Order?> GetOrderAsync(int Id, CancellationToken Cancel = default);

        Task<Order> CreateOrderAsync(User User, CartViewModel Cart, OrderVievModel OrderModel, CancellationToken Cancel = default);
    }
}
