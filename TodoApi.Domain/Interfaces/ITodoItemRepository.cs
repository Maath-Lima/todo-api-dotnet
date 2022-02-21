using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoApi.Domain.Interfaces;
using TodoApi.Domain.Models;

namespace TodoApi.Domain.Repository
{
    public interface ITodoItemRepository : IRepository<TodoItem>
    {
        Task<IEnumerable<TodoItem>> GetTodoItemsByCategory(int id);
        Task DeleteAll(IEnumerable<TodoItem> todoItemsToDelete);
    }
}
