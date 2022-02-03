using System.Net.Http.Json;
using Mapster;
using MapsterMapper;
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
        private readonly IMapper _Mapper;

        public OrdersClient(HttpClient HttpHttp, IMapper Mapper) : base(HttpHttp, "api/orders")
        {
            _Mapper = Mapper;
        }

        public async Task<IEnumerable<Order>> GetUserOrdersAsync(User User, CancellationToken Cancel = default)
        {
            var dtos = await GetAsync<IEnumerable<OrderDTO>>($"{Address}/list/{User.UserName}");
            return dtos!.Adapt<IEnumerable<Order>>();
        }

        public async Task<Order?> GetOrderAsync(int Id, CancellationToken Cancel = default)
        {
            var orderDTO = await GetAsync<Order?>($"{Address}/{Id}");
            return orderDTO!.Adapt<Order>();
        }

        public async Task<Order> CreateOrderAsync(User User, CartViewModel Cart, OrderVievModel OrderModel, CancellationToken Cancel = default)
        {
            var orderDto = _Mapper.Map<CreateOrderDTO>(Cart);
            orderDto.OrderModel = OrderModel;
            var responce = await PostAsync($"{Address}/add/{User.UserName}", orderDto);

            var dto = await responce.Content.ReadFromJsonAsync<OrderDTO>(cancellationToken: Cancel);
            return dto!.Adapt<Order>();
        }
    }
}
