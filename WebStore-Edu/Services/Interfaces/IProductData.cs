using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore_Edu.Domain.Entityes;

namespace WebStore_Edu.Services.Interfaces
{
    public interface IProductData
    {
        /// <summary> Получить все категории товаров </summary>
        IEnumerable<Section> GetSections();

        /// <summary> Получить все бренды </summary>
        IEnumerable<Brand> GetBrands();


    }
}
