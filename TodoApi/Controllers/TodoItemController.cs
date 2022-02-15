using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Domain.Interfaces;
using TodoApi.Domain.Models;
using TodoApi.Domain.Repository;
using TodoApi.DTOModels;

namespace TodoApi.Controllers
{
    [Route("api/todoItems")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITodoItemRepository _todoItemRepository;
        private readonly ITodoItemService _todoItemService;

        public TodoItemController(IMapper mapper, ITodoItemRepository todoItemRepository, ITodoItemService todoItemService)
        {
            _mapper = mapper;
            _todoItemRepository = todoItemRepository;
            _todoItemService = todoItemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            return await _todoItemRepository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(int id)
        {
            var todoItemDTO = _mapper.Map<TodoItemDTO>(await _todoItemRepository.GetById(id));

            if (todoItemDTO == null)
            {
                return NotFound();
            }

            return Ok(todoItemDTO);
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> PostTodoItem(TodoItemDTO todoItemDTO)
        {
            try
            {
                await _todoItemService.Insert(_mapper.Map<TodoItem>(todoItemDTO));

                return CreatedAtAction(nameof(GetTodoItem), new { id = todoItemDTO.Id }, todoItemDTO);
            }
            catch (Exception e)
            {

                return BadRequest(new
                {
                    success = false,
                    error = e.Message
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(int id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest(new
                {
                    success = false,
                    error = "Os ids informados não são iguais!"
                });
            }

            try
            {
                var todoItemToUpdate = await _todoItemRepository.GetById(id);

                todoItemToUpdate.Name = todoItem.Name;
                todoItemToUpdate.IsComplete = todoItem.IsComplete;
                todoItemToUpdate.TodoCategoryId = todoItem.TodoCategoryId;

                await _todoItemService.Update(todoItem);

                return Ok(
                new
                {
                    success = true,
                    data = todoItem
                });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(await TodoItemExists(id)))
                {
                    return NotFound();
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

            return NoContent();
        }

        private async Task<bool> TodoItemExists(int id)
        {
            var todoItem = await _todoItemRepository.GetById(id);

            if (todoItem == null) return false;

            return true;
        }
    }
}
