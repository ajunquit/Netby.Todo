using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Netby.Todo.Site.API.Services.API;
using Netby.Todo.Site.API.Services.API.Models;

namespace Netby.Todo.Site.Pages.Tasks
{
    public class TasksModel : PageModel
    {
        private readonly TaskApiService _taskApiService;
        public List<TaskResponse> Tasks { get; set; } = new();

        [BindProperty]
        public CreateTaskRequest NewTask { get; set; } = new();

        public TasksModel(TaskApiService taskApiService)
        {
            _taskApiService = taskApiService;
        }
        public async Task OnGetAsync()
        {
            Tasks = await _taskApiService.GetTasksAsync();
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
