using FlightAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FlightAPI.Services
{
	public class TicketService
	{
		private readonly IMongoCollection<Ticket> _tickets;

		public TicketService(
		IOptions<AirLineDataBaseSettings> AirLineDataBaseSettings)
		{
			var mongoClient = new MongoClient(
				AirLineDataBaseSettings.Value.ConnectionString);

			var mongoDatabase = mongoClient.GetDatabase(
				AirLineDataBaseSettings.Value.DatabaseName);

			_tickets = mongoDatabase.GetCollection<Ticket>(
				AirLineDataBaseSettings.Value.TicketsCollectionName);
		}

		public async Task<List<Ticket>> GetTicketsAsync()
		{
			return await _tickets.Find(ticket => true).ToListAsync();
		}

		public async Task<Ticket> GetTicketByIdAsync(string id)
		{
			return await _tickets.Find(ticket => ticket.Id == id).FirstOrDefaultAsync();
		}

		public async Task<Ticket> BookTicketAsync(Ticket ticket)
		{
			await _tickets.InsertOneAsync(ticket);
			return ticket;
		}

		public async Task<bool> UpdateTicketAsync(string id, Ticket ticket)
		{
			var result = await _tickets.ReplaceOneAsync(t => t.Id == id, ticket);
			return result.ModifiedCount > 0;
		}

		public async Task<bool> CancelTicketAsync(string id)
		{
			var result = await _tickets.DeleteOneAsync(t => t.Id == id);
			return result.DeletedCount > 0;
		}
	}
}
