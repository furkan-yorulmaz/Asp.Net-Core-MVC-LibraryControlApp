using DataAccessLayer.Abstract;
using LibraryControl.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryControl.DataAccess.Abstract
{
    public interface ISaleRepository : IGenericRepository<Sale>
    {
        Task ConfirmSaleAsync(int id);
        Task<List<Sale>> GetAllBySearchAsync(string search);
    }
}
