using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoApi.Domain.Models;

namespace TodoApi.Domain.Interfaces
{
    public interface ITodoItemService
    {
        Task Insert(TodoItem todoItem);
        Task Update(TodoItem todoItem);
        Task Delete(Guid id);
    }
}
