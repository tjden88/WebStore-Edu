namespace WebStore_Edu.Domain.DTO.Products
{
    public class ProductDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        /// <summary>Путь к картинке</summary>
        public string ImageUrl { get; set; }

        public SectionDTO Section { get; set; }

        public BrandDTO? Brand { get; set; }

    }
}
