using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebStore_Edu.Domain.Entityes;
using WebStore_Edu.Domain.Identity;

namespace WebStore_Edu.DAL.Context
{
    public class WebStoreDb : IdentityDbContext<User, Role, string>
    {
        public WebStoreDb(DbContextOptions<WebStoreDb> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Section> Sections { get; set; }

        public DbSet<Employee> Employees { get; set; }
    }
}
