using WebStore_Edu.Domain.Entityes.Base.Interfaces;

namespace WebStore_Edu.Domain.Entityes.Base
{
    public abstract class NamedEntity : Entity, INamedEntity
    {
        public string Name { get; set; }
    }
}