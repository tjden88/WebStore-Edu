using System.ComponentModel.DataAnnotations;

namespace WebStore_Edu.Domain.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Display(Name = "Цена")]
        /// <summary>Цена товара</summary>
        public decimal Price { get; set; }

        [Display(Name = "Изображение")]
        /// <summary>Путь к картинке</summary>
        public string ImageUrl { get; set; }

        [Display(Name = "Категория")]
        public string Section { get; set; }

        [Display(Name = "Бренд")]
        public string? Brand { get; set; }
    }
}
