using LibraryControl.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryControl.Business.Abstract
{
    public interface ISaleService
    {
        Task<List<Sale>> GetAllAsync();
        Task<bool> ConfirmLeaseAsync(int id);
        Task<bool> CreateAsync(Sale sale);
        Task<bool> DeleteAsync(int id);
        Task<List<Sale>> GetAllBySearchAsync(string search);
    }
}
