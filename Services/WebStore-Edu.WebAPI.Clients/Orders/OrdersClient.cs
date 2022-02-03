using WebStore_Edu.Domain.Entityes.Orders;
using WebStore_Edu.Domain.Identity;
using WebStore_Edu.Domain.ViewModels;
using WebStore_Edu.Interfaces.Services;
using WebStore_Edu.WebAPI.Clients.Base;

namespace WebStore_Edu.WebAPI.Clients.Orders
{
    public class OrdersClient : BaseClient, IOrderService
    {
        public OrdersClient(HttpClient HttpHttp) : base(HttpHttp, "api/orders")
        {
        }

        public Task<IEnumerable<Order>> GetUserOrdersAsync(User User, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<Order?> GetOrderAsync(int Id, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<Order> CreateOrderAsync(User User, CartViewModel Cart, OrderVievModel OrderModel, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }
    }
}
