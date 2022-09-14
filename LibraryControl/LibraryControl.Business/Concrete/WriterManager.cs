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
    public class WriterManager : IWriterService
    {
        private IWriterRepository _writerRepository;
        public WriterManager(IWriterRepository writerRepository)
        {
            this._writerRepository = writerRepository;
        }

        public async Task<bool> CreateAsync(Writer writer)
        {
            try
            {
                await _writerRepository.CreateAsync(writer);
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
                await _writerRepository.DeleteAsync(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Writer>> GetAllAsync()
        {
            return await _writerRepository.GetAllAsync();
        }

        public async Task<List<Writer>> GetAllBySearchAsync(string search)
        {
            return await _writerRepository.GetAllBySearchAsync(search);
        }

        public async Task<Writer> GetByIdAsync(int id)
        {
            return await _writerRepository.GetByIdAsync(id);
        }
    }
}
