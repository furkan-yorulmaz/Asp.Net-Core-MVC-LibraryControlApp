using DataAccessLayer.Concrete;
using LibraryControl.DataAccess.Abstract;
using LibraryControl.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryControl.DataAccess.Concrete
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(LibraryDbContext context) : base(context)
        {

        }
        private LibraryDbContext LibraryDbContext
        {
            get { return context as LibraryDbContext; }
        }

        public async Task<List<Category>> GetAllBySearchAsync(string search)
        {
            return await LibraryDbContext.Categories
                .Where(i => i.Name.Contains(search)
                ).ToListAsync();
        }
    }
}
