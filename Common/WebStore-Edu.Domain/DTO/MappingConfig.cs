using System;
using System.Linq;
using Mapster;
using WebStore_Edu.Domain.DTO.Orders;
using WebStore_Edu.Domain.Entityes;
using WebStore_Edu.Domain.Entityes.Orders;
using WebStore_Edu.Domain.ViewModels;

namespace WebStore_Edu.Domain.DTO;

public static class MappingConfig
{
    /// <summary> Конфигурация маппинга DTO </summary>
    public static void ConfigureDTOModels(this TypeAdapterConfig config)
    {
        config.NewConfig<OrderItem, OrderItemDTO>()
            .TwoWays()
            .Map(dest => dest.ProductId, src => src.Product.Id);

        config.NewConfig<CartViewModel, CreateOrderDTO>()
            .Map(dest => dest.CartItems,
                src => src.Items
                    .Select(i => new OrderItemDTO()
                    {
                        ProductId = i.Product.Id,
                        Price = i.Product.Price,
                        Quantity = i.Quantity
                    }).ToList());

        config.NewConfig<CreateOrderDTO, CartViewModel>()
            .Map(dest => dest.Items,
                src => src.CartItems
                    .Select(dto => ValueTuple
                        .Create(new Product
                            {
                                Id = dto.ProductId,
                                Price = dto.Price
                            },
                            dto.Quantity))
                    .ToList());

    }
}