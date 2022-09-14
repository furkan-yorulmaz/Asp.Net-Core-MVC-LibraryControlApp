using LibraryControl.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryControl.Business.Abstract
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync();
        Task<List<Category>> GetAllBySearchAsync(string search);
        Task<bool> CreateAsync(Category category);
        Task<Category> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
