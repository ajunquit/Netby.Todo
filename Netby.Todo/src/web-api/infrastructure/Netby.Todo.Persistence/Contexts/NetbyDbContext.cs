namespace Netby.Todo.Persistence.Contexts
{
    using Microsoft.EntityFrameworkCore;
    using Netby.Todo.Domain.Entities;
    public class NetbyDbContext: DbContext
    {
        public DbSet<TaskItem> Tasks { get; set; }
        public NetbyDbContext(DbContextOptions<NetbyDbContext> options) : base(options)
        {

        }
    }
}
