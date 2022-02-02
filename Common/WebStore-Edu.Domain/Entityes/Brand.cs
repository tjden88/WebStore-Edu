using System.Collections.Generic;
using WebStore_Edu.Domain.Entityes.Base;

namespace WebStore_Edu.Domain.Entityes
{
    /// <summary> Бренд </summary>
    public class Brand : OrderedNamedEntity
    {
        public ICollection<Product> Products { get; set; }

    }
}