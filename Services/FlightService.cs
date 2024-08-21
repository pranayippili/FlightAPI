using FlightAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FlightAPI.Services
{
	public class FlightService
	{
		private readonly IMongoCollection<Flight> _flights;

		public FlightService(
		IOptions<AirLineDataBaseSettings> AirLineDataBaseSettings)
		{
			var mongoClient = new MongoClient(
				AirLineDataBaseSettings.Value.ConnectionString);

			var mongoDatabase = mongoClient.GetDatabase(
				AirLineDataBaseSettings.Value.DatabaseName);

			_flights = mongoDatabase.GetCollection<Flight>(
				AirLineDataBaseSettings.Value.FlightsCollectionName);
		}

		public async Task<List<Flight>> GetFlightsAsync()
		{
			return await _flights.Find(flight => true).ToListAsync();
		}

		public async Task<Flight> GetFlightByIdAsync(string id)
		{
			return await _flights.Find(flight => flight.Id == id).FirstOrDefaultAsync();
		}

		public async Task<Flight> AddFlightAsync(Flight flight)
		{
			await _flights.InsertOneAsync(flight);
			return flight;
		}

		public async Task<bool> UpdateFlightAsync(string id, Flight flight)
		{
			var result = await _flights.ReplaceOneAsync(f => f.Id == id, flight);
			return result.ModifiedCount > 0;
		}

		public async Task<bool> DeleteFlightAsync(string id)
		{
			var result = await _flights.DeleteOneAsync(f => f.Id == id);
			return result.DeletedCount > 0;
		}
	}
}
