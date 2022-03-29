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

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(int id)
        {
            var todoItemDTO = await GetTodoItemDTO(id);

            if (todoItemDTO == null)
            {
                return NotFound(new
                {
                    code = 1,
                    message = "To-do não encontrado",
                    description = $"Não existe um to-do com o id informado: {id}"
                });
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
                    return InvalidModelStateResponseError(ModelState);
                }

                await _todoItemService.Insert(_mapper.Map<TodoItem>(todoItemDTO));

                return CreatedAtAction(nameof(GetTodoItem), new { id = todoItemDTO.Id }, todoItemDTO);
            }
            catch (DbUpdateException dbe)
            {
                return StatusCode(500, new
                {
                    code = 2,
                    message = "Erro ao inserir To-do",
                    description = dbe.Message
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    code = 3,
                    message = "Erro geral da API",
                    description = e.Message
                });
            }
        }

        [HttpPut("{id:int}")]
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
                    return InvalidModelStateResponseError(ModelState);
                }

                var todoItemToUpdate = await GetTodoItemDTO(id);

                todoItemToUpdate.Name = todoItemDTO.Name;
                todoItemToUpdate.IsComplete = todoItemDTO.IsComplete;
                todoItemToUpdate.TodoCategoryId = todoItemDTO.TodoCategoryId;

                await _todoItemService.Update(_mapper.Map<TodoItem>(todoItemToUpdate));

                return Ok(new
                {
                    success = true,
                    data = todoItemDTO
                });
            }
            catch (DbUpdateException dbe)
            {
                return BadRequest(new
                {
                    code = 2,
                    message = "Erro ao atualizr To-do",
                    description = dbe.Message
                });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    code = 3,
                    message = "Erro geral da API",
                    description = e.Message
                });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            var todoItem = await GetTodoItemDTO(id);

            if (todoItem == null)
            {
                return NotFound(new
                {
                    code = 1,
                    message = "To-do não encontrado",
                    description = $"Não existe um to-do com o id informado: {id}"
                });
            }

            try
            {
                await _todoItemService.Delete(id);

                return NoContent();
            }
            catch (DbUpdateException dbe)
            {
                return BadRequest(new
                {
                    code = 4,
                    message = "Erro ao deletar To-do",
                    description = dbe.Message
                });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    code = 3,
                    message = "Erro geral da API",
                    description = e.Message
                });
            }
        }

        #region Utils
        private async Task<bool> TodoItemExists(int id)
        {
            return await GetTodoItemDTO(id) != null;
        }

        private async Task<TodoItemDTO> GetTodoItemDTO(int id)
        {
            return _mapper.Map<TodoItemDTO>(await _todoItemRepository.GetById(id));
        }
        #endregion
    }
}
