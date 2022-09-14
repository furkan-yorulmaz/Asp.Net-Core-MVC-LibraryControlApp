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
    public class BookManager : IBookService
    {
        private IBookRepository _bookRepository;
        public BookManager(IBookRepository bookRepository)
        {
            this._bookRepository = bookRepository;
        }

        public async Task<bool> CreateAsync(Book book)
        {
            try
            {
                await _bookRepository.CreateAsync(book);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await _bookRepository.DeleteAsync(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _bookRepository.GetAllAsync();
        }

        public async Task<List<Book>> GetAllByCategoryNameAsync(string categoryName, string search)
        {
            return await _bookRepository.GetAllByCategoryNameAsync(categoryName, search);

        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await _bookRepository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(Book book)
        {
            try
            {
                await _bookRepository.UpdateAsync(book);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
