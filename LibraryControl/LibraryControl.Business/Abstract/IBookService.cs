using LibraryControl.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryControl.Business.Abstract
{
    public interface IBookService
    {
        Task<List<Book>> GetAllByCategoryNameAsync(string categoryName, string search);
        Task<List<Book>> GetAllAsync();
        Task<Book> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<bool> CreateAsync(Book book);
        Task<bool> UpdateAsync(Book book);
    }
}
