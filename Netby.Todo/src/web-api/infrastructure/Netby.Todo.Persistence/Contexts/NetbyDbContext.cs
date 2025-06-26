using Microsoft.EntityFrameworkCore;

namespace Netby.Todo.Persistence.Contexts
{
    public class NetbyDbContext: DbContext
    {
        public DbSet<Task> Users { get; set; }
        public NetbyDbContext(DbContextOptions<NetbyDbContext> options) : base(options)
        {

        }
    }
}
