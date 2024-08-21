using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FlightAPI.Models
{
	public class Ticket
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; } = null!;

		[BsonElement("FlightId")]
		public string FlightId { get; set; } = null!;

		[BsonElement("PassengerName")]
		public string PassengerName { get; set; } = null!;

		[BsonElement("BookingDate")]
		public DateTime BookingDate { get; set; }
	}
}
