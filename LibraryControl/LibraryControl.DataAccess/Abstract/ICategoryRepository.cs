using DataAccessLayer.Abstract;
using LibraryControl.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryControl.DataAccess.Abstract
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<List<Category>> GetAllBySearchAsync(string search);
    }
}
