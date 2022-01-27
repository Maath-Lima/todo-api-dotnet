using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data.Context;
using TodoApi.Domain.Interfaces;
using TodoApi.Domain.Models;
using TodoApi.Domain.Repository;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoCategoryController : ControllerBase
    {
        private readonly ITodoCategoryRepository _todoCategoryRepository;
        private readonly ITodoCategoryService _todoCategoryService;

        public TodoCategoryController(ITodoCategoryRepository todoCategoryRepository, ITodoCategoryService todoCategoryService)
        {
            _todoCategoryRepository = todoCategoryRepository;
            _todoCategoryService = todoCategoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoCategory>>> GetTodoCategory()
        {
            return await _todoCategoryRepository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoCategory>> GetTodoCategory(int id)
        {
            var todoCategory = await _todoCategoryRepository.GetById(id);

            if (todoCategory == null)
            {
                return NotFound();
            }

            return todoCategory;
        }

        [HttpGet("obter-itens/{id}")]
        public async Task<ActionResult<TodoCategory>> GetTodoCategoryAndItems(int id)
        {
            return await _todoCategoryRepository.GetTodoItemByTodoCategory(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoCategory(int id, TodoCategory todoCategory)
        {
            if (id != todoCategory.Id)
            {
                return BadRequest();
            }

            try
            {
                await _todoCategoryService.Update(todoCategory);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(await TodoCategoryExists(id)))
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

        [HttpPost]
        public async Task<ActionResult<TodoCategory>> PostTodoCategory(TodoCategory todoCategory)
        {
            await _todoCategoryService.Insert(todoCategory);

            return CreatedAtAction("GetTodoCategory", new { id = todoCategory.Id }, todoCategory);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoCategory>> DeleteTodoCategory(int id)
        {
            var todoCategory = await _todoCategoryRepository.GetById(id);

            if (todoCategory == null)
            {
                return NotFound();
            }

            await _todoCategoryService.Delete(id);

            return todoCategory;
        }
        
        private async Task<bool> TodoCategoryExists(int id)
        {
            var todoCategory = await _todoCategoryRepository.GetById(id);

            if (todoCategory == null) return false;

            return true;
        }
    }
}
