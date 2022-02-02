using Microsoft.EntityFrameworkCore;
using WebStore_Edu.DAL.Context;
using WebStore_Edu.Domain;
using WebStore_Edu.Domain.Entityes;
using WebStore_Edu.Interfaces.Services;

namespace WebStore_Edu.Services.Services.InSql
{
    public class SqlProductData : IProductData
    {
        private readonly WebStoreDb _Db;

        public SqlProductData(WebStoreDb Db) => _Db = Db;

        public IEnumerable<Section> GetSections() => _Db.Sections;

        public IEnumerable<Brand> GetBrands() => _Db.Brands;

        public IEnumerable<Product> GetProducts(ProductFilter? filter = null)
        {
            IQueryable<Product> query = _Db.Products
                    .Include(p => p.Brand)
                    .Include(p => p.Section)
                ;

            if (filter is not null)
            {
                if (filter.SectionId is { } section)
                {
                    query = query.Where(p => p.SectionId == section);
                }
                if (filter.BrandId is { } brand)
                {
                    query = query.Where(p => p.BrandId == brand);
                }

                if (filter.Ids is { } ids)
                {
                    query = query.Where(p => filter.Ids.Contains(p.Id));
                }
            }



            return query;

        }

        public Product? GetProduct(int Id) =>
            _Db.Products
                .Include(p => p.Brand)
                .Include(p => p.Section)
                .FirstOrDefault(p => p.Id == Id);

        public bool Remove(int Id)
        {
            _Db.Entry(new Product() {Id = Id}).State = EntityState.Deleted;
           return  _Db.SaveChanges() > 0;
        }
    }
}
