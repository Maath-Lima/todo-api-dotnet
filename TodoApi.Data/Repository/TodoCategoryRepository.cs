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

        public async Task<TodoCategory> GetTodoCategoryIncludeTodoItems(int id)
        {
            return await DBSet
                              .AsNoTracking()
                              .Include(ti => ti.TodoItems)
                              .FirstOrDefaultAsync(tc => tc.Id == id);
        }
    }
}
