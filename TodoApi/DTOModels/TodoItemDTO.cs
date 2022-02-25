using System.ComponentModel.DataAnnotations;

namespace TodoApi.DTOModels
{
    public class TodoItemDTO : BaseDTO
    {
        public bool IsComplete { get; set; }
        public int? TodoCategoryId { get; set; }
    }
}
