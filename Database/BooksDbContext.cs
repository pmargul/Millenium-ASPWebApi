using Microsoft.EntityFrameworkCore;
using MilleniumAspWebAPI.Database.Models;

namespace MilleniumAspWebAPI.Database
{
    public class BooksDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public BooksDbContext(DbContextOptions<BooksDbContext> optionsDbContextOptions) : base(optionsDbContextOptions)
        {
          
        }
    }
}
