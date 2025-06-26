using Netby.Todo.Application.Services.Repositories;
using Netby.Todo.Application.Services.Tasks.Common;
using Netby.Todo.Application.Services.Tasks.Create;
using Netby.Todo.Application.Services.Tasks.Update;
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
        public async Task<TaskResponse> CreateTaskAsync(CreateTaskRequest request)
        {
            var taskItem = CreateTaskItem(request);

            _unitOfWork.Tasks.Insert(taskItem);
            int result = _unitOfWork.Save();

            return await Task.FromResult(MapTaskResponse(taskItem));
        }

        public async Task<TaskResponse> UpdateTaskAsync(Guid id, UpdateTaskRequest request)
        {
            var taskItem = _unitOfWork.Tasks.Get(id);
            if (taskItem == null) return null; // lanzar exception mejor

            taskItem.Title = request.Title;
            taskItem.Description = request.Description;
            taskItem.ExpirationDate = request.ExpirationDate;
            taskItem.Status = request.Status;

            _unitOfWork.Tasks.Update(taskItem);
            int result = _unitOfWork.Save();

            return await Task.FromResult(MapTaskResponse(taskItem));
        }

        private static TaskItem CreateTaskItem(CreateTaskRequest request)
        {
            return new TaskItem
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                Description = request.Description,
                ExpirationDate = request.ExpirationDate,
                Status = Domain.Enums.StatusTask.Pending,
                Title = request.Title,
            };
        }

        private static TaskResponse MapTaskResponse(TaskItem taskItem)
        {
            return new TaskResponse
            {
                Id = taskItem.Id,
                CreationDate = taskItem.CreationDate,
                Description = taskItem.Description,
                ExpirationDate = taskItem.ExpirationDate,
                Status = taskItem.Status,
                Title = taskItem.Title
            };
        }
    }
}
