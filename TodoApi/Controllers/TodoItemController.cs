using System;
using System.Collections.Generic;
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
    public class TodoItemController : MainController
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
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            return _mapper.Map<List<TodoItemDTO>>(await _todoItemRepository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(int id)
        {
            var todoItemDTO = await GetTodoItemDTO(id);

            if (todoItemDTO == null)
            {
                return NotFound();
            }

            return todoItemDTO;
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> PostTodoItem(TodoItemDTO todoItemDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return InvalidModelStateResponse(ModelState);
                }

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
        public async Task<IActionResult> PutTodoItem(int id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                return BadRequest(new
                {
                    success = false,
                    error = "Os ids informados não são iguais!"
                });
            }

            try
            {
                if (!ModelState.IsValid)
                {
                    return InvalidModelStateResponse(ModelState);
                }

                var todoItemToUpdate = await GetTodoItemDTO(id);

                todoItemToUpdate.Name = todoItemDTO.Name;
                todoItemToUpdate.IsComplete = todoItemDTO.IsComplete;
                todoItemToUpdate.TodoCategoryId = todoItemDTO.TodoCategoryId;

                await _todoItemService.Update(_mapper.Map<TodoItem>(todoItemToUpdate));

                return Ok(
                new
                {
                    success = true,
                    data = todoItemDTO
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
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            var todoItem = await GetTodoItemDTO(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            await _todoItemService.Delete(id);

            return NoContent();
        }

        private async Task<bool> TodoItemExists(int id) => await GetTodoItemDTO(id) == null ? false : true;

        private async Task<TodoItemDTO> GetTodoItemDTO(int id)
        {
            return _mapper.Map<TodoItemDTO>(await _todoItemRepository.GetById(id));
        }
    }
}
