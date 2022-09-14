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
    public class SaleRepository : GenericRepository<Sale>, ISaleRepository
    {
        public SaleRepository(LibraryDbContext context) : base(context)
        {

        }
        private LibraryDbContext LibraryDbContext
        {
            get { return context as LibraryDbContext; }
        }

        public async Task ConfirmSaleAsync(int id)
        {
            var sale = await LibraryDbContext.Sales.
                FirstOrDefaultAsync(i => i.Id == id);

            var book = await LibraryDbContext.Books
                .FirstOrDefaultAsync(i => i.Id == sale.BookId);

            book.Stock += 1;

            LibraryDbContext.Sales.Remove(sale);
            await LibraryDbContext.SaveChangesAsync();
        }

        public override async Task<List<Sale>> GetAllAsync()
        {
            return await LibraryDbContext.Sales
                .Include(i => i.Book)
                .ToListAsync();
        }

        public override async Task CreateAsync(Sale sale)
        {
            var book = await LibraryDbContext.Books
                .FirstOrDefaultAsync(i => i.Id == sale.BookId);

            if (book.Stock > 0)
            {
                await LibraryDbContext.Sales.AddAsync(sale);
                book.Stock -= 1;
                await LibraryDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Sale>> GetAllBySearchAsync(string search)
        {
            return await LibraryDbContext.Sales
                .Include(i => i.Book)
                .Where(i => i.Name.Contains(search) ||
                i.Surname.Contains(search) ||
                i.Phone.Contains(search) ||
                i.Email.Contains(search) ||
                i.Adress.Contains(search) ||
                i.Book.Name.Contains(search)
                ).ToListAsync();
        }
    }
}
