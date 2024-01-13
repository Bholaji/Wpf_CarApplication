using Microsoft.EntityFrameworkCore;
using Models;

namespace EFCoreData.Implementations
{
    public class RepositoryDB : DbContext
    {
        public DbSet<User>? User { get; set; }
        public DbSet<Product>? Product { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source=.;Initial Catalog=CarEFCoreDB;Integrated Security=True;Connect Timeout=60;Encrypt=False");
        }
    }
}