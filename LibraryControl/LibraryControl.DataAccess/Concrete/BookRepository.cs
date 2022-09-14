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
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(LibraryDbContext context):base(context)
        {

        }
        private LibraryDbContext LibraryDbContext
        {
            get { return context as LibraryDbContext; }
        }

        public override async Task<List<Book>> GetAllAsync()
        {
            return await LibraryDbContext.Books
                    .Include(i => i.Writer)
                    .Include(i => i.Category)
                    .ToListAsync();
        }

        public async Task<List<Book>> GetAllByCategoryNameAsync(string categoryName, string search)
        {
            var books = LibraryDbContext.Books
                        .Include(i => i.Writer)
                        .AsQueryable();

            if (!string.IsNullOrEmpty(categoryName))
            {
                books = books
                    .Include(i => i.Category)
                    .Where(i => i.Category.Name == categoryName).AsQueryable();
            }

            if (!string.IsNullOrEmpty(search))
            {
                books = books
                .Where(i => i.Name.Contains(search) ||
                i.Name.Contains(search) ||
                i.Description.Contains(search) ||
                i.Writer.Name.Contains(search)).AsQueryable();
            }

            return await books.ToListAsync();
        }

        public override async Task<Book> GetByIdAsync(int id)
        {
            var book = await LibraryDbContext.Books
                .Include(i => i.Writer)
                .Include(i => i.Category)
                .Include(i => i.Sales)
                .FirstOrDefaultAsync(i => i.Id == id);
            return book;
        }
    }
}
