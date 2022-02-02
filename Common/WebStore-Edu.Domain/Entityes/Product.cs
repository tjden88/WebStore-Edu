using System.ComponentModel.DataAnnotations.Schema;
using WebStore_Edu.Domain.Entityes.Base;

namespace WebStore_Edu.Domain.Entityes
{
    /// <summary> Бренд </summary>
    public class Product : OrderedNamedEntity
    {
        /// <summary>Цена товара</summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        /// <summary>Путь к картинке</summary>
        public string ImageUrl { get; set; } 

        public int SectionId { get; set; }

        public Section Section { get; set; }

        public int? BrandId { get; set; }

        public Brand Brand { get; set; }
    }
}