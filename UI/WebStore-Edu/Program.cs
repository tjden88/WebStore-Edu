using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Serilog;
using WebStore_Edu.Domain.DTO;
using WebStore_Edu.Domain.Identity;
using WebStore_Edu.Domain.ViewModels;
using WebStore_Edu.Interfaces.Services;
using WebStore_Edu.Interfaces.TestApi;
using WebStore_Edu.Services.Services.InCookies;
using WebStore_Edu.WebAPI.Clients.Employees;
using WebStore_Edu.WebAPI.Clients.Identity;
using WebStore_Edu.WebAPI.Clients.Orders;
using WebStore_Edu.WebAPI.Clients.Products;
using WebStore_Edu.WebAPI.Clients.Values;


#region Построение приложения

var builder = WebApplication.CreateBuilder(args);

// Logging
builder.Host.UseSerilog((host, log) => log.ReadFrom.Configuration(host.Configuration));
    //.MinimumLevel.Debug()
    //.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    //.Enrich.FromLogContext()
    //.WriteTo.Console()
    //.WriteTo.File(new JsonFormatter(",", true), @$".\Logs\{DateTime.Today:d}.json"));

var services = builder.Services;

services.AddControllersWithViews();

// Mapster
var config = new TypeAdapterConfig();
config.ConfigureViewModels();
config.ConfigureDTOModels();
services.AddSingleton(config);
services.AddScoped<IMapper, ServiceMapper>();

// Add Services
services.AddScoped<ICartService, InCookiesCartService>();

// WebClients
services.AddHttpClient("WebApi", client => client.BaseAddress = new Uri(builder.Configuration["API"]))
    .AddTypedClient<IValuesApiService, ValuesClient>()
    .AddTypedClient<IEmployeesData, EmployeesClient>()
    .AddTypedClient<IProductData, ProductsClient>()
    .AddTypedClient<IOrderService, OrdersClient>()
    ;

// Identity
services.AddIdentity<User, Role>()
    .AddDefaultTokenProviders();
services.AddHttpClient("WebStoreAPIIdentity", client => client.BaseAddress = new(builder.Configuration["API"]))
    .AddTypedClient<IUserStore<User>, UsersClient>()
    .AddTypedClient<IUserRoleStore<User>, UsersClient>()
    .AddTypedClient<IUserPasswordStore<User>, UsersClient>()
    .AddTypedClient<IUserEmailStore<User>, UsersClient>()
    .AddTypedClient<IUserPhoneNumberStore<User>, UsersClient>()
    .AddTypedClient<IUserTwoFactorStore<User>, UsersClient>()
    .AddTypedClient<IUserClaimStore<User>, UsersClient>()
    .AddTypedClient<IUserLoginStore<User>, UsersClient>()
    .AddTypedClient<IRoleStore<Role>, RolesClient>();


services.Configure<IdentityOptions>(opt =>
{
#if DEBUG
    opt.Password.RequireDigit = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequiredLength = 3;
    opt.Password.RequiredUniqueChars = 3;
#endif

    opt.User.RequireUniqueEmail = false;

    opt.Lockout.AllowedForNewUsers = false;
    opt.Lockout.MaxFailedAccessAttempts = 10;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
});

services.ConfigureApplicationCookie(opt =>
{
    opt.Cookie.Name = "WebStore-Edu";
    opt.Cookie.HttpOnly = true;
    opt.ExpireTimeSpan = TimeSpan.FromDays(5);
    opt.LoginPath = "/Account/Login";
    opt.AccessDeniedPath = "/Account/AccessDenied";

    opt.SlidingExpiration = true;
});

var app = builder.Build();

#endregion


#region Конвейер запросов

// Отладочная страница исключений
if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

//app.Use(async (context, next) => // 404 страница для всех неверных адресов
//{
//    await next();
//    if (context.Response.StatusCode == 404)
//    {
//        context.Request.Path = "/Home/NotFoundPage";
//        await next();
//    }
//});
app.UseStatusCodePagesWithRedirects("/Error/{0}"); // 404 страница для всех неверных адресов вариант 2

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );
});


#endregion

// Запуск приложения
app.Run();
