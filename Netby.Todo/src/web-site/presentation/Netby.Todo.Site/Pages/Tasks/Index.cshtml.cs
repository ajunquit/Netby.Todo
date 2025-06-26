using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Netby.Todo.Site.API.Services.API.Tasks;
using Netby.Todo.Site.API.Services.API.Tasks.Models;

namespace Netby.Todo.Site.Pages.Tasks
{
    public class TasksModel : PageModel
    {
        private readonly TaskApiService _taskApiService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public List<TaskResponse> Tasks { get; set; } = new();

        [BindProperty]
        public CreateTaskRequest NewTask { get; set; } = new();

        public TasksModel(
            TaskApiService taskApiService,
            IHttpContextAccessor httpContextAccessor)
        {
            _taskApiService = taskApiService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task OnGetAsync()
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("JwtToken");

            Tasks = await _taskApiService.GetTasksAsync(token);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            var result = await _taskApiService.CreateTaskAsync(NewTask);
            if (result != null)
            {
                return RedirectToPage();
            }
            return Page();
        }
    }
}
