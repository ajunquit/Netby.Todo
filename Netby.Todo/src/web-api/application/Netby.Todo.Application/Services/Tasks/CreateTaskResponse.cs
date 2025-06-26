using Netby.Todo.Domain.Enums;

namespace Netby.Todo.Application.Services.Tasks
{
    public class CreateTaskResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime ExpirationDate { get; set; }
        public StatusTask Status { get; set; } = StatusTask.Pending;
    }
}
