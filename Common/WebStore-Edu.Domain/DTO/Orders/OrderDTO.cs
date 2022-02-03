using System;
using System.Collections.Generic;
using WebStore_Edu.Domain.Entityes.Orders;

namespace WebStore_Edu.Domain.DTO.Orders;

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