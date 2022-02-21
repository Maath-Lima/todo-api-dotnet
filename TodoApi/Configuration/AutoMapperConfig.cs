using AutoMapper;
using TodoApi.Domain.Models;
using TodoApi.DTOModels;

namespace TodoApi.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<TodoItem, TodoItemDTO>().ReverseMap();
            CreateMap<TodoCategory, TodoCategoryDTO>().ReverseMap();
        }
    }
}
