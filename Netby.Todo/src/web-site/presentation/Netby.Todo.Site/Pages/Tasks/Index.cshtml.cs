using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Netby.Todo.Site.API.Services.API.Tasks;
using Netby.Todo.Site.API.Services.API.Tasks.Enums;
using Netby.Todo.Site.API.Services.API.Tasks.Models;

namespace Netby.Todo.Site.Pages.Tasks
{
    public class TasksModel : PageModel
    {
        private readonly TaskApiService _taskApiService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public string ModalTitle = "Create/Edit";
        public List<TaskResponse> Tasks { get; set; } = new();

        [BindProperty]
        public TaskModalModel TaskModal { get; set; } = new();

        [BindProperty]
        public Guid DeleteTaskId { get; set; }

        public TasksModel(
            TaskApiService taskApiService,
            IHttpContextAccessor httpContextAccessor)
        {
            _taskApiService = taskApiService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var token = GetToken();
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Login");
            }

            Tasks = await _taskApiService.GetTasksAsync(token);
            return Page();
        }

        public async Task<IActionResult> OnPostSaveAsync()
        {

            if (TaskModal.Id == Guid.Empty)
            {
                var createRequest = new CreateTaskRequest
                {
                    Title = TaskModal.Title,
                    Description = TaskModal.Description,
                    ExpirationDate = TaskModal.ExpirationDate,
                    Status = TaskModal.Status
                };
                await _taskApiService.CreateTaskAsync(createRequest, GetToken());
                TempData["Success"] = "Task Created!";
            }
            else
            {
                var editRequest = new EditTaskRequest
                {
                    Id = TaskModal.Id,
                    Title = TaskModal.Title,
                    Description = TaskModal.Description,
                    ExpirationDate = TaskModal.ExpirationDate,
                    Status = TaskModal.Status
                };
                await _taskApiService.UpdateTaskAsync(editRequest.Id, editRequest, GetToken());
                TempData["Success"] = "Task Updated!";
            }

            return RedirectToPage();
        }


        public async Task<IActionResult> OnPostDeleteAsync()
        {
            await _taskApiService.DeleteTaskAsync(DeleteTaskId, GetToken());

            TempData["Success"] = "Task Deleted!";
            
            return RedirectToPage();
        }

        private string? GetToken()
        {
            return _httpContextAccessor.HttpContext?.Session.GetString("JwtToken");
        }
    }

    public class TaskModalModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime ExpirationDate { get; set; }
        public StatusTask Status { get; set; }
    }

}
