using CRUD_API.Model;
using Microsoft.EntityFrameworkCore;

namespace CRUD_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
            .HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId);
            // Add your seed data here if needed

            modelBuilder.Entity<Author>().HasData
                (
                new Author() 
                {
                    AuthorId=1,
                    Name = "Niche"
                }, new Author()
                {
                    AuthorId = 2,
                    Name = "Tolestoy"
                }
                );
            modelBuilder.Entity<Book>().HasData
                (
                new Book()
                {
                    BookId = 1,
                    Title = "when niche cryed",
                    AuthorId = 1,
                },
                new Book()
                {
                    BookId = 2,
                    Title = "War And Blood",
                    AuthorId = 2,
                }
                );
        }
    }

}
