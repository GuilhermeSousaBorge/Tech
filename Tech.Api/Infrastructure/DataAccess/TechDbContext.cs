using Microsoft.EntityFrameworkCore;
using Tech.Api.Model;

namespace Tech.Api.Infrastructure.DataAccess
{
    public class TechDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Checkout> Checkouts { get; set; }

    }
}
