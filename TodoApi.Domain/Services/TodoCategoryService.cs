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
        private readonly ITodoItemRepository _todoItemRepository;

        public TodoCategoryService(ITodoCategoryRepository todoCategoryRepository, ITodoItemRepository todoItemRepository)
        {
            _todoCategoryRepository = todoCategoryRepository;
            _todoItemRepository = todoItemRepository;
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
            var todoItemsToDelete = _todoItemRepository.GetTodoItemsByCategory(id);

            if (todoItemsToDelete != null)
            {
                await _todoItemRepository.DeleteAll(todoItemsToDelete.Result);
            }

            await _todoCategoryRepository.Delete(id);
        }
    }
}
