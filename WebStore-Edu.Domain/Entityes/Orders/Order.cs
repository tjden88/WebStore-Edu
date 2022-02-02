using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebStore_Edu.Domain.Entityes.Base;
using WebStore_Edu.Domain.Identity;

namespace WebStore_Edu.Domain.Entityes.Orders
{
    public class Order : Entity
    {
        [Required]
        public User User { get; set; }

        [Required]
        [MaxLength(20)]
        public string Phone { get; set; }

        [Required]
        [MaxLength(200)]
        public string Address { get; set; }

        public string Notes { get; set; }

        public DateTimeOffset Date { get; set; } = DateTimeOffset.Now;

        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();

        public decimal TotalPrice => OrderItems.Sum(i => i.TotalItemsPrice);
    }
}
