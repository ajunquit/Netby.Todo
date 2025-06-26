namespace Netby.Todo.Application.Services.Repositories
{
    public interface IUnitOfWork
    {
        ITaskRepository Tasks { get; }
        int Save();
        //Task<int> Save(CancellationToken cancellationToken);
    }
}
