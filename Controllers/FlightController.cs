using FlightAPI.Models;
using FlightAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FlightController : ControllerBase
	{
		private readonly FlightService _flightService;

		public FlightController(FlightService flightService)
		{
			_flightService = flightService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Flight>>> GetFlights()
		{
			var flights = await _flightService.GetFlightsAsync();
			return Ok(flights);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Flight>> GetFlight(string id)
		{
			var flight = await _flightService.GetFlightByIdAsync(id);
			if (flight == null)
			{
				return NotFound();
			}
			return Ok(flight);
		}

		[HttpPost]
		public async Task<ActionResult<Flight>> AddFlight(Flight flight)
		{
			var newFlight = await _flightService.AddFlightAsync(flight);
			return CreatedAtAction(nameof(GetFlight), new { id = newFlight.Id }, newFlight);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateFlight(string id, Flight flight)
		{
			var updated = await _flightService.UpdateFlightAsync(id, flight);
			if (!updated)
			{
				return NotFound();
			}
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteFlight(string id)
		{
			var deleted = await _flightService.DeleteFlightAsync(id);
			if (!deleted)
			{
				return NotFound();
			}
			return NoContent();
		}
	}
}
