using WebStore_Edu.Domain.Entityes.Base;

namespace WebStore_Edu.Domain.Entityes
{
    /// <summary> Бренд </summary>
    public class Product : OrderedNamedEntity
    {
        /// <summary>Цена товара</summary>
        public decimal Price { get; set; }

        /// <summary>Путь к картинке</summary>
        public string ImageUrl { get; set; } 

        public int SectionId { get; set; }

        public int BrandId { get; set; }
    }
}