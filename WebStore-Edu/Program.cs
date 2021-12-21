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
services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();

var app = builder.Build();

#endregion


#region Конвейер запросов

// Отладочная страница исключений
if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.UseStaticFiles();

app.UseRouting();

app.MapDefaultControllerRoute(); // Home Controller

#endregion

// Запуск приложения
app.Run();
