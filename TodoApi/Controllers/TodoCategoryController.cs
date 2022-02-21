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
    [Route("api/todoCategories")]
    [ApiController]
    public class TodoCategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITodoCategoryRepository _todoCategoryRepository;
        private readonly ITodoCategoryService _todoCategoryService;

        public TodoCategoryController(IMapper mapper,ITodoCategoryRepository todoCategoryRepository, ITodoCategoryService todoCategoryService)
        {
            _todoCategoryRepository = todoCategoryRepository;
            _todoCategoryService = todoCategoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoCategoryDTO>>> GetTodoCategory()
        {
            return _mapper.Map<List<TodoCategoryDTO>>(await _todoCategoryRepository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoCategoryDTO>> GetTodoCategory(int id)
        {
            var todoCategoryDTO = await GetTodoCategoryDTO(id);

            if (todoCategoryDTO == null)
            {
                return NotFound();
            }

            return todoCategoryDTO;
        }

        [HttpPost]
        public async Task<ActionResult<TodoCategoryDTO>> PostTodoCategory(TodoCategoryDTO todoCategoryDTO)
        {
            try
            {
                await _todoCategoryService.Insert(_mapper.Map<TodoCategory>(todoCategoryDTO));

                return CreatedAtAction(nameof(GetTodoCategory), new { id = todoCategoryDTO.Id }, todoCategoryDTO);
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
        public async Task<IActionResult> PutTodoCategory(int id, TodoCategoryDTO todoCategoryDTO)
        {
            if (id != todoCategoryDTO.Id)
            {
                return BadRequest(new
                {
                    success = false,
                    error = "Os ids informados não são iguais!"
                });
            }

            try
            {
                var todoCategoryToUpdate = await GetTodoCategoryDTO(id);

                todoCategoryToUpdate.Name = todoCategoryDTO.Name;

                await _todoCategoryService.Update(_mapper.Map<TodoCategory>(todoCategoryToUpdate));

                return Ok(
                new
                {
                    success = true,
                    data = todoCategoryDTO
                });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(await TodoCategoryExists(id)))
                {
                    return NotFound();
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoCategory(int id)
        {
            var todoCategory = await GetTodoCategoryDTO(id);

            if (todoCategory == null)
            {
                return NotFound();
            }

            await _todoCategoryService.Delete(id);

            return NoContent();
        }
        
        private async Task<bool> TodoCategoryExists(int id) => await GetTodoCategoryDTO(id) == null ? false : true;

        private async Task<TodoCategoryDTO> GetTodoCategoryDTO(int id)
        {
            return _mapper.Map<TodoCategoryDTO>(await _todoCategoryRepository.GetById(id));
        }
    }
}
