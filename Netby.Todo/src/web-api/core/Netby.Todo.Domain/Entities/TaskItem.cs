namespace Netby.Todo.Domain.Entities
{
    using Netby.Todo.Domain.Enums;

    public class TaskItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime ExpirationDate { get; set; }
        public StatusTask Status { get; set; } = StatusTask.Pending;
    }
}
