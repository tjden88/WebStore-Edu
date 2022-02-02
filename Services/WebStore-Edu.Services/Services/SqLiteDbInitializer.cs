using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebStore_Edu.DAL.Context;
using WebStore_Edu.Domain.Identity;
using WebStore_Edu.Interfaces.Services;
using WebStore_Edu.Services.Data;

namespace WebStore_Edu.Services.Services
{
    public class SqLiteDbInitializer: IDbInitializer
    {
        private readonly WebStoreDb _Db;
        private readonly ILogger<SqLiteDbInitializer> _Logger;
        private readonly UserManager<User> _UserManager;
        private readonly RoleManager<Role> _RoleManager;

        public SqLiteDbInitializer(WebStoreDb Db, ILogger<SqLiteDbInitializer> Logger, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _Db = Db;
            _Logger = Logger;
            _UserManager = userManager;
            _RoleManager = roleManager;
        }

        public async Task InitializeAsync(bool RemoveBefore = false, CancellationToken Cancel = default)
        {
            if (RemoveBefore) await RemoveAsync(Cancel).ConfigureAwait(false);

            _Logger.LogInformation("Инициализация БД...");

            await _Db.Database.ExecuteSqlRawAsync("PRAGMA journal_mode=DELETE;", Cancel).ConfigureAwait(false);

            var pendingMigrations = await _Db.Database.GetPendingMigrationsAsync(Cancel).ConfigureAwait(false);
            if (pendingMigrations.Any())
            {
                _Logger.LogInformation("Создание структуры БД...");
                await _Db.Database.MigrateAsync(Cancel).ConfigureAwait(false);
                _Logger.LogInformation("Структура БД создана");
            }


            await InitializeStartDataAsync(Cancel).ConfigureAwait(false);

            await InitializeUsersRoles(Cancel).ConfigureAwait(false);

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
                await _Db.SaveChangesAsync(Cancel).ConfigureAwait(false);
                await transaction.CommitAsync(Cancel).ConfigureAwait(false);
            }
            _Logger.LogInformation("Добавление категорий товаров выполнено");


            _Logger.LogInformation("Добавление брендов...");
            await using (var transaction = await _Db.Database.BeginTransactionAsync(Cancel).ConfigureAwait(false))
            {
                await _Db.Brands.AddRangeAsync(TestData.Brands, Cancel).ConfigureAwait(false);
                await _Db.SaveChangesAsync(Cancel).ConfigureAwait(false);
                await transaction.CommitAsync(Cancel).ConfigureAwait(false);
            }
            _Logger.LogInformation("Добавление брендов выполнено");


            _Logger.LogInformation("Добавление товаров...");
            await using (var transaction = await _Db.Database.BeginTransactionAsync(Cancel).ConfigureAwait(false))
            {
                await _Db.Products.AddRangeAsync(TestData.Products, Cancel).ConfigureAwait(false);
                await _Db.SaveChangesAsync(Cancel).ConfigureAwait(false);
                await transaction.CommitAsync(Cancel).ConfigureAwait(false);
            }
            _Logger.LogInformation("Добавление товаров выполнено");

            _Logger.LogInformation("Добавление сотрудников...");
            await using (var transaction = await _Db.Database.BeginTransactionAsync(Cancel).ConfigureAwait(false))
            {
                var employees = TestData.Employees;
                employees.ForEach(e => e.Id = 0);
                await _Db.Employees.AddRangeAsync(employees, Cancel).ConfigureAwait(false);
                await _Db.SaveChangesAsync(Cancel).ConfigureAwait(false);
                await transaction.CommitAsync(Cancel).ConfigureAwait(false);
            }
            _Logger.LogInformation("Добавление сотрудников выполнено");

        }

        private async Task InitializeUsersRoles(CancellationToken Cancel)
        {
            _Logger.LogInformation("Инициализация ролей пользователей...");

            async Task CheckRole(string RoleName)
            {
                if (!await _RoleManager.RoleExistsAsync(RoleName))
                {
                    _Logger.LogInformation("Добавление роли {0}", RoleName);
                    await _RoleManager.CreateAsync(new Role() { Name = RoleName });
                    _Logger.LogInformation("Роль {0} добавлена", RoleName);
                }

            }

            await CheckRole(Role.Administrators);
            await CheckRole(Role.Users);

            if (await _UserManager.FindByNameAsync(User.Administrator) is null)
            {
                _Logger.LogInformation("Добавление администратора");
                var admin = new User()
                {
                    UserName = User.Administrator
                };

                var adminCreateResult = await _UserManager.CreateAsync(admin, User.DefaultAdminPassword);

                if (!adminCreateResult.Succeeded)
                {
                    var errors = String.Join(", ", adminCreateResult.Errors.Select(e => e.Description));
                    _Logger.LogError("Не удалось создать учётную запись администратора! {0}", errors);
                    throw new InvalidOperationException($"Не удалось создать учётную запись администратора! Ошибки: {errors}");
                }

                await _UserManager.AddToRoleAsync(admin, Role.Administrators);

                _Logger.LogWarning("Администратор добавлен с паролем по умолчанию");
            }
        }

    }
}
