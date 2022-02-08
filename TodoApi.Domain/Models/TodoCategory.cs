﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApi.Domain.Models
{
    public class TodoCategory : Entity
    {
        public List<TodoItem> TodoItems { get; set; }

        public TodoCategory()
        {
            TodoItems = new List<TodoItem>();
        }
    }
}