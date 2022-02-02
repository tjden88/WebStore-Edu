using System.ComponentModel.DataAnnotations;

namespace WebStore_Edu.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

 
        [Required(ErrorMessage = "Имя обязательно")]
        [Display(Name = "Имя")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина имени должна быть от 2 до 50 символов")]
        public string FirstName { get; set; }

 
        [Required(ErrorMessage = "Фамилия обязательна")]
        [Display(Name = "Фамилия")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Длина фамилии должна быть от 1 до 50 символов")]
        //[RegularExpression("")]
        public string LastName { get; set; }

 
        [Display(Name = "Отчество")]
        public string? Patronymic { get; set; }

  
        [Required(ErrorMessage = "Должность обязательна")]
        [Display(Name = "Должность")]
        public string Position { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        [Display(Name = "Дата рождения")]
        public DateTime Birthday { get; set; } = DateTime.Now.AddYears(-20);

        //[Range(18,150)] // Ограничение числовых данных
        [Display(Name = "Возраст")]
        public int Age => GetAge();

        private int GetAge()
        {
            DateTime now = DateTime.Today;
            int age = now.Year - Birthday.Year;
            if (Birthday > now.AddYears(-age)) age--;
            return age;
        }

    }
}
