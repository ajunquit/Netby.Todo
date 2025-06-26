namespace Netby.Todo.Persistence.Repositories
{
    using Netby.Todo.Application.Services.Repositories;
    using Netby.Todo.Domain.Entities;
    using Netby.Todo.Persistence.Contexts;
    public class TaskRepository : Repository<TaskItem>, ITaskRepository
    {
        public TaskRepository(NetbyDbContext netbyDbContext): base(netbyDbContext)
        {
        }
    }
}
