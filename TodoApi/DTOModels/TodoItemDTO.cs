namespace TodoApi.DTOModels
{
    public class TodoItemDTO
    {
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public int? TodoCategoryId { get; set; }
    }
}
