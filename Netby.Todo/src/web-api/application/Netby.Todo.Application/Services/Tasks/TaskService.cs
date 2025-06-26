using Netby.Todo.Application.Services.Repositories;
using Netby.Todo.Application.Services.Tasks.Common;
using Netby.Todo.Application.Services.Tasks.Create;
using Netby.Todo.Application.Services.Tasks.Update;
using Netby.Todo.Domain.Entities;
using Netby.Todo.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace Netby.Todo.Application.Services.Tasks
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TaskService> _logger;

        public TaskService(
            IUnitOfWork unitOfWork,
            ILogger<TaskService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<TaskResponse> CreateTaskAsync(CreateTaskRequest request)
        {
            _logger.LogInformation("Iniciando creación de tarea: {@Request}", request);

            var taskItem = CreateTaskItem(request);

            _unitOfWork.Tasks.Insert(taskItem);
            int result = _unitOfWork.Save();

            if (result > 0)
            {
                _logger.LogInformation("Tarea creada exitosamente. Id: {TaskId}, Título: {Title}", taskItem.Id, taskItem.Title);
            }
            else
            {
                _logger.LogError("No se pudo guardar la tarea en la base de datos. Request: {@Request}", request);
            }

            return await Task.FromResult(MapTaskResponse(taskItem));
        }

        public async Task<TaskResponse> UpdateTaskAsync(Guid id, UpdateTaskRequest request)
        {
            _logger.LogInformation("Intentando actualizar tarea con Id: {TaskId}", id);

            var taskItem = _unitOfWork.Tasks.Get(id);
            if (taskItem == null)
            {
                _logger.LogWarning("No se encontró la tarea a actualizar. Id: {TaskId}", id);
                throw new NotFoundException("Not found Task Item");
            }

            taskItem.Title = request.Title;
            taskItem.Description = request.Description;
            taskItem.ExpirationDate = request.ExpirationDate;
            taskItem.Status = request.Status;

            _unitOfWork.Tasks.Update(taskItem);
            int result = _unitOfWork.Save();

            if (result > 0)
            {
                _logger.LogInformation("Tarea actualizada exitosamente. Id: {TaskId}", taskItem.Id);
            }
            else
            {
                _logger.LogError("No se pudo actualizar la tarea en la base de datos. Id: {TaskId}", taskItem.Id);
            }

            return await Task.FromResult(MapTaskResponse(taskItem));
        }

        public async Task<IEnumerable<TaskResponse>> GetAllTasksAsync()
        {
            _logger.LogInformation("Obteniendo listado de todas las tareas...");
            var tasks = _unitOfWork.Tasks.GetAll();
            var responses = tasks.Select(MapTaskResponse);
            _logger.LogInformation("Se encontraron {Count} tareas.", responses.Count());
            return await Task.FromResult(responses);
        }

        public async Task<bool> DeleteTaskAsync(Guid id)
        {
            _logger.LogInformation("Intentando eliminar tarea con Id: {TaskId}", id);

            var taskItem = _unitOfWork.Tasks.Get(id);
            if (taskItem == null)
            {
                _logger.LogWarning("No se encontró la tarea a eliminar. Id: {TaskId}", id);
                return false;
            }

            _unitOfWork.Tasks.Delete(taskItem.Id);
            int result = _unitOfWork.Save();

            if (result > 0)
            {
                _logger.LogInformation("Tarea eliminada exitosamente. Id: {TaskId}", taskItem.Id);
                return await Task.FromResult(true);
            }
            else
            {
                _logger.LogError("No se pudo eliminar la tarea de la base de datos. Id: {TaskId}", taskItem.Id);
                return await Task.FromResult(false);
            }
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
