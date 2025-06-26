using Netby.Todo.Application.Services.Repositories;
using Netby.Todo.Domain.Entities;

namespace Netby.Todo.Application.Services.Tasks
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaskService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<CreateTaskResponse> CreateTaskAsync(CreateTaskRequest request)
        {
            var taskItem = new TaskItem
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                Description = request.Description,
                ExpirationDate = request.ExpirationDate,
                Status = Domain.Enums.StatusTask.Pending,
                Title = request.Title,
            };

            _unitOfWork.Tasks.Insert(taskItem);
            int result = _unitOfWork.Save();
            
            return await Task.FromResult(new CreateTaskResponse { 
                Id = taskItem.Id,
                CreationDate = taskItem.CreationDate,
                Description = taskItem.Description,
                ExpirationDate = taskItem.ExpirationDate,
                Status = taskItem.Status,
                Title = taskItem.Title
            });
        }
    }
}
