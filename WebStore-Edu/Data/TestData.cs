using WebStore_Edu.Models;

namespace WebStore_Edu.Data
{
    /// <summary> Тестовые данные </summary>
    public static class TestData
    {
        /// <summary> Список сотрудников </summary>
        public static List<Employee> Employees => new()
        {
            new()
            {
                Id = 0,
                FirstName = "Иван",
                Patronymic = "Васильевич",
                LastName = "Грозный",
                Birthday = DateTime.Parse("15.10.1957"),
                Position = "Начальник Всея Руси"
            },
            new()
            {
                Id = 1,
                FirstName = "Василий",
                Patronymic = "Петрович",
                LastName = "Васильев",
                Birthday = DateTime.Parse("03.03.1980"),
                Position = "Менеджер"
            },
            new()
            {
                Id = 2,
                FirstName = "Денис",
                Patronymic = "Владимирович",
                LastName = "Дульцев",
                Birthday = DateTime.Parse("18.08.1988"),
                Position = "Студент GB"
            }
        };
    }
}
