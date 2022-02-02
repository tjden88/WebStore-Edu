using System.ComponentModel.DataAnnotations;
using WebStore_Edu.Domain.Identity;

namespace WebStore_Edu.ViewModels
{
    public class OrderVievModel
    {
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required]
        public string Address { get; set; }

        public string Notes { get; set; }

        public User User { get; set; }

        public CartViewModel Cart { get; set; }
    }
}
