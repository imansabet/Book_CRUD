using CRUD_API.Model;
using Microsoft.EntityFrameworkCore;

namespace CRUD_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<AuthorDTO> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
    }

}
