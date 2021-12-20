﻿namespace WebStore_Edu.Models;
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

    /// <summary> Статус </summary>
    public string Status { get; set; }

    /// <summary> Возраст </summary>
    public int Age => GetAge();

    private int GetAge()
    {
        DateTime now = DateTime.Today;
        int age = now.Year - Birthday.Year;
        if (Birthday > now.AddYears(-age)) age--;
        return age;
    }

}
