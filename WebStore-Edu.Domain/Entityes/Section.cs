using WebStore_Edu.Domain.Entityes.Base;

namespace WebStore_Edu.Domain.Entityes
{
    /// <summary> Категория товара </summary>
    public class Section : OrderedNamedEntity
    {
        /// <summary>ID родительской секции</summary>
        public int ParentId { get; set; }
    }
}