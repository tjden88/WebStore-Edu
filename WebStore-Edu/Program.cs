using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebStore_Edu.DAL.Context;
using WebStore_Edu.Domain.Entityes;
using WebStore_Edu.Domain.Identity;
using WebStore_Edu.Services;
using WebStore_Edu.Services.InSql;
using WebStore_Edu.Services.Interfaces;
using WebStore_Edu.ViewModels;


#region Построение приложения

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddControllersWithViews();

// Mapster
var config = new TypeAdapterConfig();

config.ForType<Product, ProductViewModel>()
    .Map(dest => dest.Section,
        src => src.Section.Name)
    .Map(dest => dest.Brand,
        src => src.Brand.Name).Compile();


services.AddSingleton(config);
services.AddScoped<IMapper, ServiceMapper>();

// Add Services
services.AddScoped<IEmployeesData, SqlEmployeesData>();
services.AddScoped<IProductData, SqlProductData>();
services.AddScoped<IDbInitializer, DbInitializer>();

services.AddIdentity<User, Role>() // Identity
    .AddEntityFrameworkStores<WebStoreDb>()
    .AddDefaultTokenProviders();

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
    opt.LoginPath = "/Account/Authorize";
    opt.AccessDeniedPath = "/Account/AccessDenied";

    opt.SlidingExpiration = true;
});

// Add db
services.AddDbContext<WebStoreDb>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

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

app.MapDefaultControllerRoute(); // Home Controller



#endregion

// Инициализация данных БД

await using (var scope = app.Services.CreateAsyncScope())
{
    await scope.ServiceProvider.GetRequiredService<IDbInitializer>().InitializeAsync();
}


// Запуск приложения
app.Run();
