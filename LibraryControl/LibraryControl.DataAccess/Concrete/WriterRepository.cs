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
    public class WriterRepository : GenericRepository<Writer>, IWriterRepository
    {
        public WriterRepository(LibraryDbContext context) : base(context)
        {

        }
        private LibraryDbContext LibraryDbContext
        {
            get { return context as LibraryDbContext; }
        }

        public async Task<List<Writer>> GetAllBySearchAsync(string search)
        {
            return await LibraryDbContext.Writers
                .Where(i => i.Name.Contains(search)
                ).ToListAsync();
        }
    }
}
