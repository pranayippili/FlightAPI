﻿namespace FlightAPI.Models;

public class AirLineDataBaseSettings
{
		public string ConnectionString { get; set; } = null!;
		public string DatabaseName { get; set; } = null!;
		public string FlightsCollectionName { get; set; } = null!;
		public string TicketsCollectionName { get; set; } = null!;
}
