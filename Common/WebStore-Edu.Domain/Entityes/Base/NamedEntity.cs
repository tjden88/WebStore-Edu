using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WebStore_Edu.Domain.Entityes.Base.Interfaces;

namespace WebStore_Edu.Domain.Entityes.Base
{
    [Index(nameof(Name), IsUnique = true)]
    public abstract class NamedEntity : Entity, INamedEntity
    {
        [Required]
        public string Name { get; set; }
    }
}