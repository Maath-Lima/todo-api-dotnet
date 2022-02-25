using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TodoApi.DTOModels
{
    public class TodoCategoryDTO : BaseDTO
    {
        public IEnumerable<TodoItemDTO> TodoItems { get; set; }
    }
}
