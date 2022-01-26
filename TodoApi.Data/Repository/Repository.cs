using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Data.Context;
using TodoApi.Domain.Interfaces;
using TodoApi.Domain.Models;

namespace TodoApi.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly TodoContext DB;
        protected readonly DbSet<TEntity> DBSet;

        public Repository(TodoContext Db)
        {
            DB = Db;
            DBSet = Db.Set<TEntity>();
        }
        public async Task<TEntity> GetById(int id)
        {
            return await DBSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await DBSet.ToListAsync();
        }

        public async Task Insert(TEntity todoItemEntity)
        {
            DBSet.Add(todoItemEntity);
            await SaveChanges();
        }

        public async Task Delete(int id)
        {
            DBSet.Remove(new TEntity { Id = id });
            await SaveChanges();
        }

        public async Task Update(TEntity todoItemEntity)
        {
            DBSet.Update(todoItemEntity);
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await DB.SaveChangesAsync();
        }

        public void Dispose()
        {
            DB?.Dispose();
        }
    }
}
