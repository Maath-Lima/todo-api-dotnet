using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApi.Domain.Models
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
