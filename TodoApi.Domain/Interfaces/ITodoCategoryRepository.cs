using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Domain.Interfaces;
using TodoApi.Domain.Models;

namespace TodoApi.Domain.Repository
{
    public interface ITodoCategoryRepository : IRepository<TodoCategory>
    {
        Task<TodoCategory> GetTodoItemByTodoCategory(int id);
    }
}
