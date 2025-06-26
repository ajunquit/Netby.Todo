namespace Netby.Todo.Application.Services.Tasks.Create
{
    public class CreateTaskRequest
    {
        public string Title { get; set; }
        public string Description { get; set; } = null!;
        public DateTime ExpirationDate { get; set; }
    }
}
