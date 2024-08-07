using Microsoft.EntityFrameworkCore;
using Vuelos.Data;
using Vuelos.Models;

namespace Vuelos.Repository
{
    public interface IVuelosRepository
    {
        Task<bool> AddAsync(VuelosModel model);
        Task<IEnumerable<VuelosModel>> GetAllAsync();
        Task<IEnumerable<string>> GetFlightNumberAsync();
        Task DeleteFlightAsync(string flightNumber);
        Task UpdateFlightAsync(VuelosModel model);
        Task<bool>ExistAnyAsync (string flightNumber);
        Task<VuelosModel> GetByFlightNumberAsync(string flightNumber);
    }

    public class VuelosRepository : IVuelosRepository
    {
        private readonly VuelosDbContext _context;

        public VuelosRepository(VuelosDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(VuelosModel model)
        {
            try
            {
                await _context.Vuelos.AddAsync(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw new Exception("No se pudo agregar un vuelo");
            }

        }

        public async Task<IEnumerable<VuelosModel>> GetAllAsync()
        {
            try
            {
                var res = await _context.Vuelos.ToListAsync();
                return res;
            }
            catch (Exception)
            {
                throw new Exception("No se pueden recuperar todos los vuelos");
            }
        }

        public async Task<IEnumerable<string>> GetFlightNumberAsync()
        {
            try
            {
                var res = await _context.Vuelos
                                 .Select(v => v.FlightNumber)
                                 .ToListAsync();
                return res;
            }
            catch (Exception)
            {
                throw new Exception("No se puede obtener el numero del vuelo");
            }
        }
        public async Task<VuelosModel> GetByFlightNumberAsync(string flightNumber)
        {
            var res = await _context.Vuelos
                .FirstOrDefaultAsync(v => v.FlightNumber == flightNumber) ?? throw new Exception("No hay un vuelo con ese número");
            return res;
        }

        public async Task DeleteFlightAsync(string flightNumber)
        {
            try
            {
                var flight = await GetByFlightNumberAsync(flightNumber);
                if (flight != null)
                {
                    _context.Vuelos.Remove(flight);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("No hay un vuelo con ese número");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el vuelo: {ex.Message}");
            }
        }

        public async Task UpdateFlightAsync(VuelosModel model)
        {
            try
            {
                var existingFlight = await GetByFlightNumberAsync(model.FlightNumber);
                if (existingFlight == null)
                {
                    throw new Exception("No hay un vuelo con ese número");
                }

                existingFlight.CheckIn = model.CheckIn;
                existingFlight.Airline = model.Airline;
                existingFlight.IsDelayed = model.IsDelayed;

                _context.Entry(existingFlight).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el vuelo: {ex.Message}");
            }
        }

        public async Task<bool> ExistAnyAsync(string flightNumber)
        {
            return await _context.Vuelos.AnyAsync(v => v.FlightNumber == flightNumber);
        }
    }
}