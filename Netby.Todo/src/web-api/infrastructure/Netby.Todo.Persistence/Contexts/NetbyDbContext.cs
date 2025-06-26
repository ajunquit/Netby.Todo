namespace Netby.Todo.Persistence.Contexts
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Netby.Todo.Domain.Entities;
    public class NetbyDbContext: IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public DbSet<TaskItem> Tasks { get; set; }
        public NetbyDbContext(DbContextOptions<NetbyDbContext> options) : base(options)
        {

        }
    }
}
