namespace Netby.Todo.Site.API.Services.API.Models
{
    public class CreateTaskRequest
    {
        public string Title { get; set; }
        public string Description { get; set; } = null!;
        public DateTime ExpirationDate { get; set; }
    }
}
