using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Domain.Models;

namespace TodoApi.Domain.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task Insert(TEntity entity);
        Task<TEntity> GetById(int id);
        Task<List<TEntity>> GetAll();
        Task Update(TEntity entity);
        Task Delete(int id);
        Task<int> SaveChanges();
    }
}
