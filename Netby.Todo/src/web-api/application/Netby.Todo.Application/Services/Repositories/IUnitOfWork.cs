namespace Netby.Todo.Application.Services.Repositories
{
    public interface IUnitOfWork
    {
        ITaskRepository Tasks { get; }
        Task<int> Save(CancellationToken cancellationToken);
    }
}
