using Netby.Todo.Domain.Enums;

namespace Netby.Todo.Application.Services.Tasks.Update
{
    public class UpdateTaskRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime ExpirationDate { get; set; }
        public StatusTask Status { get; set; }
    }
}
