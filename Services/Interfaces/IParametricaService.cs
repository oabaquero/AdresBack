using Adres.Models;

namespace Adres.Services.Interfaces
{
    public interface IParametricaService
    {
        Task<List<Parametrica>> GetList(string tipo);
    }
}