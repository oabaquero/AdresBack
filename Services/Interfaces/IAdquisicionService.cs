using Adres.Models;

namespace Adres.Services.Interfaces
{
    public interface IAdquisicionService
    {
        Task<List<Adquisicion>> GetList();
        Task<Adquisicion> Get(int adquisicionId);
        Task<Adquisicion> Add(Adquisicion adquisicion);
        Task<bool> Update(Adquisicion adquisicion);
        Task<bool> Delete(Adquisicion adquisicion);
        Task<List<Historico>> GetListHistorico(int adquisicionId);
        Task GuardarHistorico(string adquisicion, bool registroNuevo, string adquisicionAnterior, int adquisicionId);
    }
}