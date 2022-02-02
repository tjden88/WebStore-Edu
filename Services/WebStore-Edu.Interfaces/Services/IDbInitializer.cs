namespace WebStore_Edu.Interfaces.Services
{
    /// <summary>
    /// Инициализатор БД
    /// </summary>
    public interface IDbInitializer
    {
        Task InitializeAsync(bool RemoveBefore = false, CancellationToken Cancel = default);

        Task<bool> RemoveAsync(CancellationToken Cancel = default);
    }
}
