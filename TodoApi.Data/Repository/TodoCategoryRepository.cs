using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApi.Data.Context;
using TodoApi.Domain.Models;
using TodoApi.Domain.Repository;

namespace TodoApi.Data.Repository
{
    public class TodoCategoryRepository : Repository<TodoCategory>, ITodoCategoryRepository
    {


        public TodoCategoryRepository(TodoContext Db)
            : base(Db)
        {
        }

        public async Task<TodoCategory> GetTodoItemByTodoCategory(int id)
        {
            return await Db.TodoCategory
                                        .AsNoTracking()
                                        .Where(tc => tc.Id == id)
                                        .Select(tc => new TodoCategory
                                        {
                                            Id = tc.Id,
                                            Name = tc.Name,
                                            TodoItems = tc
                                                .TodoItems
                                                .Select(ti => new TodoItem { Id = ti.Id, Name = ti.Name, IsComplete = ti.IsComplete })
                                                .ToList()
                                        })
                                        .FirstOrDefaultAsync();
        }
    }
}
