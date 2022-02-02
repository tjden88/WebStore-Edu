using System.ComponentModel.DataAnnotations;

namespace WebStore_Edu.Domain.ViewModels
{
    public class OrderVievModel
    {
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required]
        public string Address { get; set; }

        public string? Notes { get; set; }
    }
}
