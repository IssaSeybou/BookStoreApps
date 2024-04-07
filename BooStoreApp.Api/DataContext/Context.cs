using Microsoft.EntityFrameworkCore;

namespace BooStoreApp.Api.DataContext
{
    public class Context:DbContext
    {
        public Context(DbContextOptions<Context> options):base(options) { }
        
    }
}
