using System.ComponentModel.DataAnnotations;
using WebStore_Edu.Domain.Identity;

namespace WebStore_Edu.ViewModels
{
    public class OrderVievModel
    {
        [Microsoft.Build.Framework.Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Microsoft.Build.Framework.Required]
        public string Address { get; set; }

        public string Notes { get; set; }

        User User { get; set; }
    }
}
