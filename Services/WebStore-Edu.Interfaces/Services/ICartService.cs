using WebStore_Edu.Domain.ViewModels;

namespace WebStore_Edu.Interfaces.Services
{
    public interface ICartService
    {
        void Add(int Id, int Quantity = 1);

        void Decrement(int Id);

        void Remove(int Id);

        void Clear();

        CartViewModel CreateViewModel();

        int ProductsCount();
    }
}
