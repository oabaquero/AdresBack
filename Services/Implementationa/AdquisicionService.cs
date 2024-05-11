using Adres.Models;
using Adres.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Adres.Services.Implementations
{
    public class AdquisicionService : IAdquisicionService
    {
        private readonly AdresContext _dbContext;
        public AdquisicionService(AdresContext dbContext){
            _dbContext = dbContext;
        }
        public async Task<List<Adquisicion>> GetList()
        {
            try
            {
                return await _dbContext.Adquisiciones
                .Include(x => x.Unidades)
                .Include(x => x.Bienes)
                .Include(x => x.Proveedores)
                .ToListAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Adquisicion> Get(int IAdquisicion)
        {
            try
            {
                return await _dbContext.Adquisiciones
                .Include(x => x.Unidades)
                .Include(x => x.Bienes)
                .Include(x => x.Proveedores)
                .Where(x => x.Id == IAdquisicion).FirstOrDefaultAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Adquisicion> Add(Adquisicion adquisicion)
        {
            try
            {
                _dbContext.Adquisiciones.Add(adquisicion);
                await _dbContext.SaveChangesAsync();
                return adquisicion;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Update(Adquisicion adquisicion)
        {
            try
            {
                _dbContext.Adquisiciones.Update(adquisicion);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Delete(Adquisicion adquisicion)
        {
            try
            {
                _dbContext.Adquisiciones.Remove(adquisicion);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}