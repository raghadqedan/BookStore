using BookStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data
{

    public class ApplicationDbContext : IdentityDbContext
    {
       public DbSet<Author> Authors { get; set; }
       public DbSet<Category> Categories { get; set; }
       public DbSet<Book> Books { get; set; }
       public DbSet<BookCategory> BookCategory { get; set; }
       public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<BookCategory>().HasKey(e => new
            {
                e.BookId,
                e.CategoryId,
            });

        }

    }
}