namespace WebStore_Edu.Models;
public class Employee
{
    public int Id { get; set;}

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Patronymic { get; set; }

    /// <summary> Должность </summary>
    public string Position { get; set; }

    /// <summary> Дата рождения </summary>
    public DateTime Birthday { get; set; }

}
