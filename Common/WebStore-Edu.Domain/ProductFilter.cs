namespace WebStore_Edu.Domain
{
    /// <summary>
    /// Фильтр выборки товаров из БД
    /// </summary>
    public class ProductFilter
    {
        public int? SectionId { get; set; }

        public int? BrandId { get; set; }

        public int[]? Ids { get; set; }

    }
}
