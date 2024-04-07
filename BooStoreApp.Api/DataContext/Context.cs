using BooStoreApp.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BooStoreApp.Api.DataContext
{
    public class Context:DbContext
    {
        public Context(DbContextOptions<Context> options):base(options) { }
        public DbSet<Author> Author { get; set; }
        public DbSet<Book> Books { get; set; }
        
    }
}
