using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using WebStore_Edu.DAL.Context;
using WebStore_Edu.Services;
using WebStore_Edu.Services.InSql;
using WebStore_Edu.Services.Interfaces;


#region Построение приложения

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddControllersWithViews();

// Mapster
var config = new TypeAdapterConfig();
services.AddSingleton(config);
services.AddScoped<IMapper, ServiceMapper>();

// Add Services
services.AddScoped<IEmployeesData, SqlEmployeesData>();
services.AddScoped<IProductData, SqlProductData>();
services.AddScoped<IDbInitializer, DbInitializer>();

// Add db
services.AddDbContext<WebStoreDb>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

var app = builder.Build();

#endregion


#region Конвейер запросов

// Отладочная страница исключений
if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.Use(async (context, next) => // 404 страница для всех неверных адресов
{
    await next();
    if (context.Response.StatusCode == 404)
    {
        context.Request.Path = "/Home/NotFoundPage";
        await next();
    }
});

app.UseStaticFiles();

app.UseRouting();

app.MapDefaultControllerRoute(); // Home Controller



#endregion

// Инициализация данных БД

await using (var scope = app.Services.CreateAsyncScope())
{
    await scope.ServiceProvider.GetRequiredService<IDbInitializer>().InitializeAsync();
}


// Запуск приложения
app.Run();
