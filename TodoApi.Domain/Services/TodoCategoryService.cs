using System;
using System.Threading.Tasks;
using TodoApi.Domain.Interfaces;
using TodoApi.Domain.Models;
using TodoApi.Domain.Repository;

namespace TodoApi.Domain.Services
{
    public class TodoCategoryService : ITodoCategoryService
    {
        private readonly ITodoCategoryRepository _todoCategoryRepository;

        public TodoCategoryService(ITodoCategoryRepository todoCategoryRepository)
        {
            _todoCategoryRepository = todoCategoryRepository;
        }

        public async Task Insert(TodoCategory todoItem)
        {
            await _todoCategoryRepository.Insert(todoItem);
        }

        public async Task Update(TodoCategory todoItem)
        {
            await _todoCategoryRepository.Update(todoItem);
        }

        public async Task Delete(int id)
        {
            

            await _todoCategoryRepository.Delete(id);
        }
    }
}
