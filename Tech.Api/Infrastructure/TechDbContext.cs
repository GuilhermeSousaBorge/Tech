using Microsoft.EntityFrameworkCore;
using Tech.Api.Model;

namespace Tech.Api.Infrastructure
{
    public class TechDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=C:\\Users\\Guilherme\\Desktop\\Workspace\\Tech\\Tech.Api\\Infrastructure\\TechLibraryDb.db ");
        }
    }
}
