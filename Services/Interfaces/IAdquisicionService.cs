using Adres.Models;

namespace Adres.Services.Interfaces
{
    public interface IAdquisicionService
    {
        Task<List<Adquisicion>> GetList();
        Task<Adquisicion> Get(int IAdquisicion);
        Task<Adquisicion> Add(Adquisicion adquisicion);
        Task<bool> Update(Adquisicion adquisicion);
        Task<bool> Delete(Adquisicion adquisicion);
    }
}