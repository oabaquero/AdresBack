using Adres.Models;
using Adres.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Adres.Services.Implementations
{
    public class ParametricaService : IParametricaService
    {
        private readonly AdresContext _dbContext;
        public ParametricaService(AdresContext dbContext){
            _dbContext = dbContext;
        }
        public async Task<List<Parametrica>> GetList(string tipo)
        {
            try
            {
                return await _dbContext.Parametricas.Where(x=>x.Tipo == tipo).ToListAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}