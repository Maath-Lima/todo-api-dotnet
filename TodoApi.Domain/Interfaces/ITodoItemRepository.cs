using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoApi.Domain.Models;

namespace TodoApi.Domain.Repository
{
    public interface ITodoItemRepository
    {
        Task Insert(TodoItem todoItemEntity);
        Task<TodoItem> GetById(Guid id);
        Task<List<TodoItem>> GetAll();
        Task Update(TodoItem todoItemEntity);
        Task Delete(Guid id);
        Task<int> SaveChanges();
    }
}
