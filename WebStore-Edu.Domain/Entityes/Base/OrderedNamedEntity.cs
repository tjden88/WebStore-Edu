using WebStore_Edu.Domain.Entityes.Base.Interfaces;

namespace WebStore_Edu.Domain.Entityes.Base
{
    public class OrderedNamedEntity : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }
    }
}