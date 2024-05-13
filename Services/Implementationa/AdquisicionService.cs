using Adres.Models;
using Adres.Services.Interfaces;
using JsonDiffPatchDotNet;
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

        public async Task<Adquisicion> Get(int adquisicionId)
        {
            try
            {
                return await _dbContext.Adquisiciones
                .Include(x => x.Unidades)
                .Include(x => x.Bienes)
                .Include(x => x.Proveedores)
                .Where(x => x.Id == adquisicionId).FirstOrDefaultAsync();
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
                adquisicion.Id = await _dbContext.Adquisiciones.CountAsync() + 1;
                await _dbContext.Adquisiciones.AddAsync(adquisicion);
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

        public async Task<List<Historico>> GetListHistorico(int adquisicionId)
        {
            try
            {
                return await _dbContext.Historicos
                .Where(x=> x.AdquisicionId == adquisicionId)
                .ToListAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task GuardarHistorico(string adquisicion, bool registroNuevo, string adquisicionAnterior, int adquisicionId)
        {
            // Obtener el próximo Id de Historico de manera segura
            var siguienteId = await _dbContext.Historicos.CountAsync() + 1;

            var historicoData = new Historico
            {
                Id = siguienteId,
                AdquisicionId = adquisicionId,
                DataActual = adquisicion,
                FechaModificacion = DateTime.UtcNow
            };

            if (registroNuevo)
            {
                historicoData.DataAnterior = string.Empty;
                historicoData.Diferencia = "Nuevo registro";
            }
            else
            {
                historicoData.DataAnterior = adquisicionAnterior;
                
                var jdp = new JsonDiffPatch();
                var diferencia = jdp.Diff(historicoData.DataAnterior, historicoData.DataActual);
                historicoData.Diferencia = diferencia ?? "No hay diferencia detectada";
            }

            // Agregar el historial a la base de datos
            await _dbContext.Historicos.AddAsync(historicoData);
            await _dbContext.SaveChangesAsync();
        }
    }
}