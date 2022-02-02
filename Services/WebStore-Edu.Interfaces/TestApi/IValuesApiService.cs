namespace WebStore_Edu.Interfaces.TestApi
{
    public interface IValuesApiService
    {
        IEnumerable<string> GetAll();

        string? Get(int Id);

        int Count();

        void Add(string Value);

        void Update(int Id, string Value);

        bool Delete(int Id);
    }
}
