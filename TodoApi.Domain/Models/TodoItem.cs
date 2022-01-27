using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Domain.Models
{
    public class TodoItem : Entity
    {
        public bool IsComplete { get; set; } = false;

        public int? TodoCategoryId { get; set; }
        public TodoCategory? TodoCategory { get; set; }
    }
}
