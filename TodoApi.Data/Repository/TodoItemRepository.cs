using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Data.Context;
using TodoApi.Domain.Models;
using TodoApi.Domain.Repository;

namespace TodoApi.Data.Repository
{
    public class TodoItemRepository : Repository<TodoItem>, ITodoItemRepository
    {
        public TodoItemRepository(TodoContext Db) 
            : base(Db)
        {
        }

        public async Task<IEnumerable<TodoItem>> GetTodoItemsByCategory(int id)
        {
            return await DBSet.Where(ti => ti.TodoCategoryId == id).ToListAsync();
        }

        public async Task DeleteAll(IEnumerable<TodoItem> todoItemsToDelete)
        {
            Db.RemoveRange(todoItemsToDelete);

            await SaveChanges();
        }
    }
}
