using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Domain.Interfaces;
using TodoApi.Domain.Models;
using TodoApi.Domain.Repository;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        private readonly ITodoItemRepository _todoItemRepository;
        private readonly ITodoItemService _todoItemService;

        public TodoItemController(ITodoItemRepository todoItemRepository, ITodoItemService todoItemService)
        {
            _todoItemRepository = todoItemRepository;
            _todoItemService = todoItemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            return await _todoItemRepository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(int id)
        {
            var todoItem = await _todoItemRepository.GetById(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            await _todoItemService.Insert(todoItem);

            return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(int id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            try
            {
                await _todoItemService.Update(todoItem);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(await TodoItemExists(id)))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItem>> DeleteTodoItem(int id)
        {
            var todoItem = await _todoItemRepository.GetById(id);
            
            if (todoItem == null)
            {
                return NotFound();
            }

            await _todoItemService.Delete(id);

            return todoItem;
        }

        private async Task<bool> TodoItemExists(int id)
        {
            var todoItem = await _todoItemRepository.GetById(id);

            if (todoItem == null) return false;

            return true;
        }
    }
}
