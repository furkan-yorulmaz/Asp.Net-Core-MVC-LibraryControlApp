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
    public class SaleManager : ISaleService
    {
        private ISaleRepository _saleRepository;
        public SaleManager(ISaleRepository saleRepository)
        {
            this._saleRepository = saleRepository;
        }

        public async Task<bool> ConfirmLeaseAsync(int id)
        {
            try
            {
                await _saleRepository.ConfirmSaleAsync(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> CreateAsync(Sale sale)
        {
            try
            {
                await _saleRepository.CreateAsync(sale);
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
                await _saleRepository.DeleteAsync(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Task<List<Sale>> GetAllAsync()
        {
            return _saleRepository.GetAllAsync();
        }

        public async Task<List<Sale>> GetAllBySearchAsync(string search)
        {
            return await _saleRepository.GetAllBySearchAsync(search);
        }
    }
}
