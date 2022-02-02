using System;
using WebStore_Edu.Domain.Entityes.Base;

namespace WebStore_Edu.Domain.Entityes;
public class Employee : Entity
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Patronymic { get; set; }

    /// <summary> Должность </summary>
    public string Position { get; set; }

    /// <summary> Дата рождения </summary>
    public DateTime Birthday { get; set; }
}
