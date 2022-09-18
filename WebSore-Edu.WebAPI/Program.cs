using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebStore_Edu.DAL.Context;
using WebStore_Edu.Domain.DTO;
using WebStore_Edu.Domain.DTO.Orders;
using WebStore_Edu.Domain.Identity;
using WebStore_Edu.Domain.ViewModels;
using WebStore_Edu.Interfaces.Services;
using WebStore_Edu.Services.Services;
using WebStore_Edu.Services.Services.InSql;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

// Mapster
var config = new TypeAdapterConfig();
config.ConfigureViewModels();
config.ConfigureDTOModels();
services.AddSingleton(config);
services.AddScoped<IMapper, ServiceMapper>();

// Add Services
services.AddScoped<IEmployeesData, SqlEmployeesData>();
services.AddScoped<IProductData, SqlProductData>();
services.AddScoped<IOrderService, SqlOrderService>();

// Identity
services.AddIdentity<User, Role>() 
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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

await using var scope = app.Services.CreateAsyncScope();

await scope.ServiceProvider.GetRequiredService<IDbInitializer>().InitializeAsync();

app.Run();
