using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using TodoApi.Data.Context;
using TodoApi.Data.Repository;
using TodoApi.Domain.Interfaces;
using TodoApi.Domain.Repository;
using TodoApi.Domain.Services;

namespace TodoApi.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<TodoContext>();

            services.AddScoped<ITodoItemRepository, TodoItemRepository>();
            services.AddScoped<ITodoCategoryRepository, TodoCategoryRepository>();

            services.AddScoped<ITodoItemService, TodoItemService>();
            services.AddScoped<ITodoCategoryService, TodoCategoryService>();
            
            return services;
        }
    }
}
