
using System.ComponentModel.DataAnnotations;

namespace WebStore_Edu.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "Поле обязательно")]
        [Display(Name ="Имя пользователя")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Поле обязательно")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле обязательно")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Пароли не совпадают")]
        [Display(Name = "Подтверждение пароля")]
        public string ConfirmPassword { get; set; }
    }
}
