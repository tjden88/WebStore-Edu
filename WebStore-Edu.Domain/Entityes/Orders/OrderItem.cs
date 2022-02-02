using System.ComponentModel.DataAnnotations;
using WebStore_Edu.Domain.Entityes.Base;

namespace WebStore_Edu.Domain.Entityes.Orders;

public class OrderItem : Entity
{
    [Required]
    public Product Product { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public Order Order { get; set; }

    public decimal TotalItemsPrice => Price * Quantity;
}