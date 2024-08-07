using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Vuelos.Data;
using Vuelos.Models;
using Vuelos.Repository;

namespace Vuelos.Tests
{
    public class VuelosRepositoryTests
    {
        private VuelosDbContext GetInMemoryDbContext()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<VuelosDbContext>()
                .UseSqlite(connection)
                .Options;

            var context = new VuelosDbContext(options);
            context.Database.EnsureCreated();

            return context;
        }

        private async Task AddFlightAsync(VuelosDbContext context, string FlightNumber, DateTime CheckIn, string Airline, bool IsDelayed)
        {
            var vuelo = new VuelosModel
            {
                FlightNumber = FlightNumber,
                CheckIn = CheckIn,
                Airline = Airline,
                IsDelayed = IsDelayed
            };

            await context.Vuelos.AddAsync(vuelo);
            await context.SaveChangesAsync();
        }

        [Fact]
        public async Task AddAsync_ShouldAddFlightSuccessfully()
        {
            var context = GetInMemoryDbContext();
            var repository = new VuelosRepository(context);
            const string FlightNumber = "FL123";
            const string Airline = "Test Airline";
            var CheckIn = DateTime.Now.AddHours(+2);
            const bool IsDelayed = false;

            await AddFlightAsync(context, FlightNumber , CheckIn, Airline, IsDelayed);
            var result = await context.Vuelos.FirstOrDefaultAsync(v => v.FlightNumber == FlightNumber);

            Assert.NotNull(result);
            Assert.Equal(FlightNumber, result.FlightNumber);
            Assert.Equal(Airline, result.Airline);
            Assert.Equal(CheckIn, result.CheckIn);
            Assert.False(result.IsDelayed);
        }

        [Fact]
        public async Task UpdateFlightAsync_ShouldUpdateFlightSuccessfully()
        {
            var context = GetInMemoryDbContext();
            var repository = new VuelosRepository(context);
            const string FlightNumber = "FL123";
            const string Airline = "Test Airline";
            const string UpdatedAirline = "Updated Airline";
            var CheckIn = DateTime.Now.AddHours(+2);
            var UpdatedCheckIn = DateTime.Now.AddHours(+4);
            const bool IsDelayed = false;
            var updatedFlight = new VuelosModel
            {
                FlightNumber = FlightNumber,
                CheckIn = UpdatedCheckIn,
                Airline = UpdatedAirline,
                IsDelayed = IsDelayed
            };

            await AddFlightAsync(context, FlightNumber, CheckIn, Airline, IsDelayed);
            await repository.UpdateFlightAsync(updatedFlight);
            var result = await context.Vuelos.FirstOrDefaultAsync(v => v.FlightNumber == FlightNumber);

            Assert.NotNull(result);
            Assert.Equal(FlightNumber, result.FlightNumber);
            Assert.Equal(UpdatedAirline, result.Airline);
            Assert.Equal(UpdatedCheckIn, result.CheckIn);
            Assert.False(result.IsDelayed);
            Assert.Equal(updatedFlight.CheckIn, result.CheckIn);
        }
        [Fact]
        public async Task DeleteFlightAsync_ShouldDeleteFlightSuccessfully()
        {
            var context = GetInMemoryDbContext();
            var repository = new VuelosRepository(context);
            const string FlightNumber = "FL123";
            const string Airline = "Test Airline";
            var CheckIn = DateTime.Now.AddHours(+2);
            const bool IsDelayed = false;

            await AddFlightAsync(context, FlightNumber, CheckIn, Airline, IsDelayed);
            await repository.DeleteFlightAsync(FlightNumber);
            var result = await context.Vuelos.FirstOrDefaultAsync(v => v.FlightNumber == FlightNumber);

            Assert.Null(result);
        }
    }
}
