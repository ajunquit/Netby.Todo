using Netby.Todo.Application.Services.Tasks.Common;
using Netby.Todo.Application.Services.Tasks.Create;
using Netby.Todo.Application.Services.Tasks.Update;

namespace Netby.Todo.Application.Services.Tasks
{
    public interface ITaskService
    {
        Task<TaskResponse> CreateTaskAsync(CreateTaskRequest request);
        Task<TaskResponse> UpdateTaskAsync(Guid id, UpdateTaskRequest request);
        Task<IEnumerable<TaskResponse>> GetAllTasksAsync();
        Task<bool> DeleteTaskAsync(Guid id);
    }
}
