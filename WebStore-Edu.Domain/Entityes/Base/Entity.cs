using WebStore_Edu.Domain.Entityes.Base.Interfaces;

namespace WebStore_Edu.Domain.Entityes.Base
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
