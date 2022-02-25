namespace TodoApi.Domain.Models
{
    public class TodoItem : Entity
    {
        public bool IsComplete { get; set; }

        public int? TodoCategoryId { get; set; }

        public TodoCategory TodoCategory { get; set; }
    }
}
