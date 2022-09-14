using LibraryControl.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryControl.Business.Abstract
{
    public interface IWriterService
    {
        Task<List<Writer>> GetAllAsync();
        Task<List<Writer>> GetAllBySearchAsync(string search);
        Task<bool> CreateAsync(Writer writer);
        Task<Writer> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
