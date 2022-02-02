using System.Collections.Generic;
using WebStore_Edu.Domain.Entityes.Base;

namespace WebStore_Edu.Domain.Entityes
{
    /// <summary> Категория товара </summary>
    public class Section : OrderedNamedEntity
    {
        /// <summary>ID родительской категории</summary>
        public int? ParentId { get; set; }

        public Section Parent { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}