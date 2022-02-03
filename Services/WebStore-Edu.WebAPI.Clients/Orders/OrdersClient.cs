using System.Net.Http.Json;
using WebStore_Edu.Domain.DTO;
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

        public async Task<IEnumerable<Order>> GetUserOrdersAsync(User User, CancellationToken Cancel = default)
        {
            return (await GetAsync<IEnumerable<Order>>($"{Address}/list/{User.UserName}"))!;
        }

        public async Task<Order?> GetOrderAsync(int Id, CancellationToken Cancel = default)
        {
            return await GetAsync<Order?>($"{Address}/{Id}");
        }

        public async Task<Order> CreateOrderAsync(User User, CartViewModel Cart, OrderVievModel OrderModel, CancellationToken Cancel = default)
        {
            var orderDto = new CreateOrderDTO()
            {
                Cart = Cart,
                OrderModel = OrderModel
            };
            var responce = await PostAsync($"{Address}/add/{User.UserName}", orderDto);

            return (await responce.Content.ReadFromJsonAsync<Order>(cancellationToken: Cancel))!;
        }
    }
}
