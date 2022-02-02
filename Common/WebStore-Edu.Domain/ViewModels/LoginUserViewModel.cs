using System.ComponentModel.DataAnnotations;

namespace WebStore_Edu.Domain.ViewModels
{
    public class LoginUserViewModel
    {
        [Required(ErrorMessage = "Имя пользователя обязательно")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Пароль обязателен")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool Remember { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
