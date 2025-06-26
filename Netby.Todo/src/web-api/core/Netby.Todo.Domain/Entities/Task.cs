namespace Netby.Todo.Domain.Entities
{
    using Netby.Todo.Domain.Enums;
    public class Task
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public DateTime ExpirationDate { get; set; }
        public StatusTask Status { get; set; }
    }
}
