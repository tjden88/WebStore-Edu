using System.ComponentModel.DataAnnotations;

namespace WebStore_Edu.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

 
        [Required]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

 
        [Required]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

 
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

  
        [Required]
        [Display(Name = "Должность")]
        public string Position { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        [Display(Name = "Дата рождения")]
        public DateTime Birthday { get; set; }


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
