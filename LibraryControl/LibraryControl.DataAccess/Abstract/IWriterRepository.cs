using DataAccessLayer.Abstract;
using LibraryControl.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryControl.DataAccess.Abstract
{
    public interface IWriterRepository:IGenericRepository<Writer>
    {
        Task<List<Writer>> GetAllBySearchAsync(string search);
    }
}
