using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebStore_Edu.DAL.Context;
using WebStore_Edu.Domain.Identity;
using WebStore_Edu.Domain.ViewModels;
using WebStore_Edu.Interfaces.Services;
using WebStore_Edu.Services;
using WebStore_Edu.Services.Services;
using WebStore_Edu.Services.Services.InCookies;
using WebStore_Edu.Services.Services.InSql;


#region Построение приложения

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddControllersWithViews();

// Mapster
var config = new TypeAdapterConfig();

config.ConfigureViewModels();

services.AddSingleton(config);
services.AddScoped<IMapper, ServiceMapper>();

// Add Services
services.AddScoped<IEmployeesData, SqlEmployeesData>();
services.AddScoped<IProductData, SqlProductData>();
services.AddScoped<ICartService, InCookiesCartService>();
services.AddScoped<IOrderService, SqlOrderService>();

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
    opt.LoginPath = "/Account/Login";
    opt.AccessDeniedPath = "/Account/AccessDenied";

    opt.SlidingExpiration = true;
});

// Add db

var db = builder.Configuration["Database"];

switch (db)
{
    case "SqlServer":
        services.AddDbContext<WebStoreDb>(opt =>
            opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
        services.AddScoped<IDbInitializer, SqlServerDbInitializer>();
        break;

    case "SqLite":
        services.AddDbContext<WebStoreDb>(opt =>
            opt.UseSqlite(builder.Configuration.GetConnectionString("SqLite"),
                o => o.MigrationsAssembly("WebStore-Edu.DAL.SqLite")));
        services.AddScoped<IDbInitializer, SqLiteDbInitializer>();
        break;

    default:
        throw new InvalidOperationException($"Тип БД {db} не поддерживается");
}


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

// Инициализация данных БД

await using (var scope = app.Services.CreateAsyncScope())
{
    await scope.ServiceProvider.GetRequiredService<IDbInitializer>().InitializeAsync();
}


// Запуск приложения
app.Run();
