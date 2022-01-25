using Mapster;
using MapsterMapper;
using WebStore_Edu.Services;
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
services.AddScoped<IEmployeesData, InMemoryEmployeesData>();
services.AddSingleton<IProductData, InMemoryProductData>();

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

// Запуск приложения
app.Run();
