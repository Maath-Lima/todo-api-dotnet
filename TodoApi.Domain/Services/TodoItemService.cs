using System;
using System.Threading.Tasks;
using TodoApi.Domain.Interfaces;
using TodoApi.Domain.Models;
using TodoApi.Domain.Repository;

namespace TodoApi.Domain.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemRepository _todoItemRepository;

        public TodoItemService(ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }

        public async Task Insert(TodoItem todoItem)
        {
            await _todoItemRepository.Insert(todoItem);
        }

        public async Task Update(TodoItem todoItem)
        {
            await _todoItemRepository.Update(todoItem);
        }
        
        public async Task Delete(int id)
        {
            await _todoItemRepository.Delete(id);
        }
    }
}
