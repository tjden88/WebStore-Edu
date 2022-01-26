using WebStore_Edu.Domain.Entityes;

namespace WebStore_Edu.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        /// <summary>Цена товара</summary>
        public decimal Price { get; set; }

        /// <summary>Путь к картинке</summary>
        public string ImageUrl { get; set; }

        public string Section { get; set; }

        public string? Brand { get; set; }
    }
}
