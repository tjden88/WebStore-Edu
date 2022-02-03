using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using WebStore_Edu.Domain.DTO;
using WebStore_Edu.Domain.DTO.Orders;
using WebStore_Edu.Domain.ViewModels;
using WebStore_Edu.Interfaces.Services;
using WebStore_Edu.WebAPI.Clients.Products;

namespace WebStore_Edu.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {

        public MainWindow()
        {
            var services = new ServiceCollection();

            services.AddHttpClient<IProductData, ProductsClient>(cfg => cfg.BaseAddress = new("http://localhost:5001"));
            // Mapster
            var config = new TypeAdapterConfig();
            config.ConfigureViewModels();
            config.ConfigureDTOModels();
            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();

            var provider = services.BuildServiceProvider();

            var products = provider.GetRequiredService<IProductData>().GetProducts();

            InitializeComponent();

            list.ItemsSource = products;
        }
    }
}
