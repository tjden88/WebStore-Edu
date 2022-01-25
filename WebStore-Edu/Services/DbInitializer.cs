using Microsoft.EntityFrameworkCore;
using WebStore_Edu.DAL.Context;
using WebStore_Edu.Data;
using WebStore_Edu.Services.Interfaces;

namespace WebStore_Edu.Services
{
    public class DbInitializer : IDbInitializer
    {
        private readonly WebStoreDb _Db;
        private readonly ILogger<DbInitializer> _Logger;

        public DbInitializer(WebStoreDb Db, ILogger<DbInitializer> Logger)
        {
            _Db = Db;
            _Logger = Logger;
        }

        public async Task InitializeAsync(bool RemoveBefore = false, CancellationToken Cancel = default)
        {
            if (RemoveBefore) await RemoveAsync(Cancel).ConfigureAwait(false);

            _Logger.LogInformation("Инициализация БД...");

            var pendingMigrations = await _Db.Database.GetPendingMigrationsAsync(Cancel).ConfigureAwait(false);
            if (pendingMigrations.Any())
            {
                _Logger.LogInformation("Создание структуры БД...");
                await _Db.Database.MigrateAsync(Cancel).ConfigureAwait(false);
                _Logger.LogInformation("Структура БД создана");
            }


            await InitializeStartDataAsync(Cancel).ConfigureAwait(false);

            _Logger.LogInformation("Инициализация БД выполнена");
        }

        public async Task<bool> RemoveAsync(CancellationToken Cancel = default)
        {
            var result = await _Db.Database.EnsureDeletedAsync(Cancel).ConfigureAwait(false);

            _Logger
                .LogWarning(result
                ? "База данных была удалена"
                : "Неуспешная попытка удаления базы данных");

            return result;
        }


        private async Task InitializeStartDataAsync(CancellationToken Cancel)
        {
            if (await _Db.Sections.AnyAsync(Cancel))
            {
                _Logger.LogInformation("Добавление данных не требуется");
                return;
            }

            _Logger.LogInformation("Добавление категорий товаров...");

            await using (var transaction = await _Db.Database.BeginTransactionAsync(Cancel).ConfigureAwait(false))
            {
                await _Db.Sections.AddRangeAsync(TestData.Sections, Cancel).ConfigureAwait(false);
                await _Db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Sections] ON", Cancel); // TODO: плохо так делать
                await _Db.SaveChangesAsync(Cancel).ConfigureAwait(false);
                await _Db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Sections] OFF", Cancel);
                await transaction.CommitAsync(Cancel).ConfigureAwait(false);
            }
            _Logger.LogInformation("Добавление категорий товаров выполнено");


            _Logger.LogInformation("Добавление брендов...");
            await using (var transaction = await _Db.Database.BeginTransactionAsync(Cancel).ConfigureAwait(false))
            {
                await _Db.Brands.AddRangeAsync(TestData.Brands, Cancel).ConfigureAwait(false);
                await _Db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Brands] ON", Cancel);
                await _Db.SaveChangesAsync(Cancel).ConfigureAwait(false);
                await _Db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Brands] OFF", Cancel);
                await transaction.CommitAsync(Cancel).ConfigureAwait(false);
            }
            _Logger.LogInformation("Добавление брендов выполнено");


            _Logger.LogInformation("Добавление товаров...");
            await using (var transaction = await _Db.Database.BeginTransactionAsync(Cancel).ConfigureAwait(false))
            {
                await _Db.Products.AddRangeAsync(TestData.Products, Cancel).ConfigureAwait(false);
                await _Db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Products] ON", Cancel);
                await _Db.SaveChangesAsync(Cancel).ConfigureAwait(false);
                await _Db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Products] OFF", Cancel);
                await transaction.CommitAsync(Cancel).ConfigureAwait(false);
            }
            _Logger.LogInformation("Добавление товаров выполнено");
        }
    }
}
