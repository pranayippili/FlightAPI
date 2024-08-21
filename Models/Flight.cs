using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FlightAPI.Models
{
	public class Flight
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; } = null!;

		[BsonElement("FlightNumber")]
		public string FlightNumber { get; set; } = null!;

		[BsonElement("Origin")]
		public string Origin { get; set; } = null!;

		[BsonElement("Destination")]
		public string Destination { get; set; } = null!;

		[BsonElement("DepartureTime")]
		public DateTime DepartureTime { get; set; }

		[BsonElement("ArrivalTime")]
		public DateTime ArrivalTime { get; set; }
	}
}
