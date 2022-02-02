using WebStore_Edu.Domain.Entityes.Orders;
using WebStore_Edu.Domain.Identity;
using WebStore_Edu.ViewModels;

namespace WebStore_Edu.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetUserOrdersAsync(User User, CancellationToken Cancel = default);

        Task<Order?> GetOrderAsync(int Id, CancellationToken Cancel = default);

        Task<Order> CreateOrderAsync(OrderVievModel OrderModel, CancellationToken Cancel = default);
    }
}
