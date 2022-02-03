using System;
using System.Collections.Generic;
using System.Linq;
using Mapster;
using WebStore_Edu.Domain.Entityes;
using WebStore_Edu.Domain.Entityes.Orders;
using WebStore_Edu.Domain.ViewModels;

namespace WebStore_Edu.Domain.DTO
{
    public class CreateOrderDTO
    {
        public IEnumerable<OrderItemDTO> CartItems { get; set; }

        public OrderVievModel OrderModel { get; set; }
    }

    public class OrderDTO
    {
        public int Id { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Notes { get; set; }

        public DateTimeOffset Date { get; set; }

        public IEnumerable<OrderItemDTO> OrderItems { get; set; }

        public OrderStatus OrderStatus { get; set; }
    }

    public class OrderItemDTO
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

    }

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
}
