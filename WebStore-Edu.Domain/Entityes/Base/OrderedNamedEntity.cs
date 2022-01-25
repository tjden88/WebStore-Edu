using WebStore_Edu.Domain.Entityes.Base.Interfaces;

namespace WebStore_Edu.Domain.Entityes.Base
{
    public abstract class OrderedNamedEntity : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }
    }
}