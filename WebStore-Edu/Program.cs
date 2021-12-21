using Mapster;
using MapsterMapper;


#region Построение приложения

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddControllersWithViews();

var config = new TypeAdapterConfig();
services.AddSingleton(config);
services.AddScoped<IMapper, ServiceMapper>();

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
