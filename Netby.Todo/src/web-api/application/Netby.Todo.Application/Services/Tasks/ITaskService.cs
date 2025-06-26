namespace Netby.Todo.Application.Services.Tasks
{
    public interface ITaskService
    {
        Task<CreateTaskResponse> CreateTaskAsync(CreateTaskRequest request);
    }
}
