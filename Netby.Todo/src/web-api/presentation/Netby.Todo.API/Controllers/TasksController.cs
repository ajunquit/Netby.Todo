using Microsoft.AspNetCore.Mvc;
using Netby.Todo.Application.Services.Tasks;
using Netby.Todo.Application.Services.Tasks.Common;
using Netby.Todo.Application.Services.Tasks.Create;
using Netby.Todo.Application.Services.Tasks.Update;

namespace Netby.Todo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public async Task<ActionResult<TaskResponse>> Create([FromBody] CreateTaskRequest request)
        {
            var task = await _taskService.CreateTaskAsync(request);
            return CreatedAtAction(nameof(Create), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TaskResponse>> Update(Guid id, [FromBody] UpdateTaskRequest request)
        {
            var updatedTask = await _taskService.UpdateTaskAsync(id, request);
            if (updatedTask == null) return NotFound();
            return Ok(updatedTask);
        }
    }
}
