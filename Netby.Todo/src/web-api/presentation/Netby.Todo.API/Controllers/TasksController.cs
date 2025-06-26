using Microsoft.AspNetCore.Mvc;
using Netby.Todo.Application.Services.Tasks;

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
        public async Task<ActionResult<CreateTaskResponse>> Create([FromBody] CreateTaskRequest request)
        {
            var task = await _taskService.CreateTaskAsync(request);
            return CreatedAtAction(nameof(Create), new { id = task.Id }, task);
        }
    }
}
