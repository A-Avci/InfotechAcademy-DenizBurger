using Microsoft.EntityFrameworkCore;

namespace İnfotechProje.Models
{
    public class İnfotechProjeEntities : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        public İnfotechProjeEntities(DbContextOptions options) : base(options)
        {

        }
    }

}
