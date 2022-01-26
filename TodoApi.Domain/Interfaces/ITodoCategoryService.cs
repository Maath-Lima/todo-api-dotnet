using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoApi.Domain.Models;

namespace TodoApi.Domain.Interfaces
{
    public interface ITodoCategoryService
    {
        Task Insert(TodoCategory todoCategory);
        Task Update(TodoCategory todoCategory);
        Task Delete(int id);
    }
}
