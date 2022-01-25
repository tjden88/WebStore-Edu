using Microsoft.EntityFrameworkCore;
using WebStore_Edu.Domain.Entityes;

namespace WebStore_Edu.DAL.Context
{
    public class WebStoreDb : DbContext
    {
        public WebStoreDb(DbContextOptions<WebStoreDb> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Section> Sections { get; set; }
    }
}
