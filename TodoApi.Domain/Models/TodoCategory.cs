using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApi.Domain.Models
{
    public class TodoCategory : Entity
    {
        public IEnumerable<TodoItem> TodoItems { get; set; }
    }
}
