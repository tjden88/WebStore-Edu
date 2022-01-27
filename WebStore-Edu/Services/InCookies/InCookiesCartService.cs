using MapsterMapper;
using Newtonsoft.Json;
using WebStore_Edu.Domain;
using WebStore_Edu.Domain.Entityes;
using WebStore_Edu.Services.Interfaces;
using WebStore_Edu.ViewModels;

namespace WebStore_Edu.Services.InCookies
{
    public class InCookiesCartService : ICartService
    {
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private readonly IProductData _ProductData;
        private readonly IMapper _Mapper;

        private readonly string _CookieCartName;

        public InCookiesCartService(IHttpContextAccessor httpContextAccessor, IProductData ProductData, IMapper Mapper)
        {
            _HttpContextAccessor = httpContextAccessor;
            _ProductData = ProductData;
            _Mapper = Mapper;

            _CookieCartName = "WebStore-Edu.Cart";
            if (_HttpContextAccessor.HttpContext?.User.Identity!.IsAuthenticated == true)
                _CookieCartName += $"-{_HttpContextAccessor.HttpContext?.User.Identity!.Name}";
        }

        private Cart GetUserCart()
        {
            if (_HttpContextAccessor.HttpContext?.Request.Cookies[_CookieCartName] is { } cart)
            {
                return JsonConvert.DeserializeObject<Cart>(cart) ?? new Cart();
            }

            return new Cart();
        }

        private void SetUserCart(Cart cart)
        {
            _HttpContextAccessor.HttpContext?.Response.Cookies.Delete(_CookieCartName);
            _HttpContextAccessor.HttpContext?.Response.Cookies.Append(_CookieCartName, JsonConvert.SerializeObject(cart));
        }

        public void Add(int Id, int Quantity = 1)
        {
            var cart = GetUserCart();
            var item = cart.Items.FirstOrDefault(i => i.ProductId == Id);
            if (item is null)
            {
                cart.Items.Add(new CartItem()
                {
                    ProductId = Id,
                    Quantity = Quantity
                });
            }
            else
            {
                item.Quantity++;
            }
            SetUserCart(cart);
        }

        public void Decrement(int Id)
        {
            var cart = GetUserCart();
            var item = cart.Items.FirstOrDefault(i => i.ProductId == Id);

            if (item is null)
                return;

            item.Quantity--;

            if (item.Quantity < 1)
                cart.Items.Remove(item);

            SetUserCart(cart);
        }

        public void Remove(int Id)
        {
            var cart = GetUserCart();
            var item = cart.Items.FirstOrDefault(i => i.ProductId == Id);

            if (item is null)
                return;

            cart.Items.Remove(item);

            SetUserCart(cart);
        }

        public void Clear()
        {
            var cart = GetUserCart();
            cart.Items.Clear();
            SetUserCart(cart);
        }

        public CartViewModel CreateViewModel()
        {

            var cart = GetUserCart();

            var products = _ProductData.GetProducts(new ProductFilter()
            {
                Ids = cart.Items.Select(i => i.ProductId).ToArray()
            });

            var viewsDict = _Mapper
                .Map<IEnumerable<ProductViewModel>>(products)
                .ToDictionary(vm => vm.Id);

            return new()
            {
                Items = cart.Items
                    .Where(item => viewsDict.ContainsKey(item.ProductId))
                    .Select(item => (viewsDict[item.ProductId], item.Quantity))
            };

        }
    }
}
