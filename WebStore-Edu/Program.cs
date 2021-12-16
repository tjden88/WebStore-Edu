#region Построение приложения

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddControllersWithViews();
// Вопрос: почему не services.AddMvc(); ? 

var app = builder.Build();

#endregion


#region Конвейер запросов

// Отладочная страница исключений
if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.MapGet("/", () => "Привет!");

#endregion

// Запуск приложения
app.Run();
