using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoApi.Data.Context;
using TodoApi.Domain.Models;
using TodoApi.Domain.Repository;

namespace TodoApi.Data.Repository
{
    public class TodoItemRepository : ITodoItemRepository
    {
        protected readonly TodoItemContext DB;
        protected readonly DbSet<TodoItem> DBSet;

        public TodoItemRepository(TodoItemContext Db)
        {
            DB = Db;
            DBSet = Db.Set<TodoItem>();
        }

        public async Task<List<TodoItem>> GetAll()
        {
            return await DBSet.ToListAsync();
        }

        public async Task<TodoItem> GetById(Guid id)
        {
            return await DBSet.FindAsync(id);
        }

        public async Task Insert(TodoItem todoItemEntity)
        {
            DBSet.Add(todoItemEntity);
            await SaveChanges();
        }

        public async Task Delete(Guid id)
        {
            DBSet.Remove(new TodoItem { Id = id});
            await SaveChanges();
        }

        public async Task Update(TodoItem todoItemEntity)
        {
            DBSet.Update(todoItemEntity);
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await DB.SaveChangesAsync();
        }
    }
}
