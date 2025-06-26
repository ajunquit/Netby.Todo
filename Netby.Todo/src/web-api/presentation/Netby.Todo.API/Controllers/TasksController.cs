using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Netby.Todo.Application.Services.Tasks;
using Netby.Todo.Application.Services.Tasks.Common;
using Netby.Todo.Application.Services.Tasks.Create;
using Netby.Todo.Application.Services.Tasks.Update;

namespace Netby.Todo.API.Controllers
{
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskResponse>>> GetAll()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _taskService.DeleteTaskAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
