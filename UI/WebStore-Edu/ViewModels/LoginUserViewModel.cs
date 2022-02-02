using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebStore_Edu.ViewModels
{
    public class LoginUserViewModel
    {
        [Required(ErrorMessage = "Имя пользователя обязательно")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Пароль обязателен")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool Remember { get; set; }

        [HiddenInput]
        public string? ReturnUrl { get; set; }
    }
}
