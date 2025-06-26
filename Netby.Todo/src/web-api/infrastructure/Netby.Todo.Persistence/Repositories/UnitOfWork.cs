using Netby.Todo.Application.Services.Repositories;
using Netby.Todo.Persistence.Contexts;

namespace Netby.Todo.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly NetbyDbContext _netbyDbContext;

        public ITaskRepository Tasks { get; }

        public UnitOfWork(
            ITaskRepository taskRepository,
            NetbyDbContext netbyDbContext)
        {
            Tasks = taskRepository;
            _netbyDbContext = netbyDbContext;
        }

        public int Save()
        {
            return _netbyDbContext.SaveChanges();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
