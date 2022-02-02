using Mapster;
using WebStore_Edu.Domain.Entityes;

namespace WebStore_Edu.Domain.ViewModels
{
    public static class MappingConfig
    {
        /// <summary> Конфигурация маппинга вьюмоделей </summary>
        public static void ConfigureViewModels(this TypeAdapterConfig config)
        {
            config.NewConfig<Product, ProductViewModel>()
                .Map(dest => dest.Section, src => src.Section.Name)
                .Map(dest => dest.Brand, src => src.Brand.Name);
        }
    }
}
