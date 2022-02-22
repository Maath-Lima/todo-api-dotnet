using System.Collections.Generic;

namespace TodoApi.DTOModels
{
    public class TodoCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<TodoItemDTO> TodoItems { get; set; }
    }
}
