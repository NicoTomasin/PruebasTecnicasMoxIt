using Vuelos.Models;

namespace Vuelos.Interfaces
{
    public interface IVuelosRepository
    {
        Task<bool> AddAsync(VuelosModel model);
        Task<IEnumerable<VuelosModel>> GetAllAsync();
        Task<IEnumerable<string>> GetFlightNumberAsync();
        Task DeleteFlightAsync(string flightNumber);
        Task UpdateFlightAsync(VuelosModel model);
        Task<bool> ExistAnyAsync(string flightNumber);
        Task<VuelosModel> GetByFlightNumberAsync(string flightNumber);
    }
}
