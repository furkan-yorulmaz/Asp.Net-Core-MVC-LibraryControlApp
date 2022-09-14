using LibraryControl.Business.Abstract;
using LibraryControl.DataAccess.Abstract;
using LibraryControl.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryControl.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        public CategoryManager(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        public async Task<bool> CreateAsync(Category category)
        {
            try
            {
                await _categoryRepository.CreateAsync(category);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Task<List<Category>> GetAllAsync()
        {
            return _categoryRepository.GetAllAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await _categoryRepository.DeleteAsync(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task<List<Category>> GetAllBySearchAsync(string search)
        {
            return await _categoryRepository.GetAllBySearchAsync(search);
        }
    }
}
