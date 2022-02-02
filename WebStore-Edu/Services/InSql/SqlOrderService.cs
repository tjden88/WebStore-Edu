using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebStore_Edu.DAL.Context;
using WebStore_Edu.Domain.Entityes.Orders;
using WebStore_Edu.Domain.Identity;
using WebStore_Edu.Services.Interfaces;
using WebStore_Edu.ViewModels;

namespace WebStore_Edu.Services.InSql
{
    public class SqlOrderService : IOrderService
    {
        private readonly WebStoreDb _Db;
        private readonly UserManager<User> _UserManager;
        private readonly IMapper _Mapper;

        public SqlOrderService(WebStoreDb Db, UserManager<User> UserManager, IMapper Mapper)
        {
            _Db = Db;
            _UserManager = UserManager;
            _Mapper = Mapper;
        }
        public async Task<IEnumerable<Order>> GetUserOrdersAsync(User User, CancellationToken Cancel = default)
        {
            var orders = await _Db.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.User == User)
                .ToArrayAsync(Cancel)
                .ConfigureAwait(false);

            return orders;
        }

        public async Task<Order?> GetOrderAsync(int Id, CancellationToken Cancel = default)
        {
            var order = await _Db.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == Id, Cancel);
            return order;
        }

        public async Task<Order> CreateOrderAsync(CartViewModel Cart, OrderVievModel OrderModel, CancellationToken Cancel = default)
        {
            await using var transaction = await _Db.Database.BeginTransactionAsync(Cancel).ConfigureAwait(false);

            var order = _Mapper.Map<Order>(OrderModel);

            var prodIds = Cart.Items.Select(i => i.Product.Id);


            var orderItems = await _Db.Products
                .Where(p => prodIds.Contains(p.Id))
                .Join(Cart.Items,
                    prod => prod.Id,
                    cartItem => cartItem.Product.Id,
                    (p, item) => new OrderItem()
                    {
                        Id=p.Id,
                        Order = order,
                        Quantity = item.Quantity,
                        Price = p.Price,
                        Product = p
                    })
                .ToArrayAsync(Cancel)
                .ConfigureAwait(false);

            order.OrderItems = orderItems;

            await _Db.Orders.AddAsync(order, Cancel).ConfigureAwait(false);

            await _Db.SaveChangesAsync(Cancel).ConfigureAwait(false);

            await transaction.CommitAsync(Cancel).ConfigureAwait(false);

            return order;
        }
    }
}
