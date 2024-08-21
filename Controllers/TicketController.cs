using FlightAPI.Models;
using FlightAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TicketController : ControllerBase
	{
		private readonly TicketService _ticketService;

		public TicketController(TicketService ticketService)
		{
			_ticketService = ticketService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
		{
			var tickets = await _ticketService.GetTicketsAsync();
			return Ok(tickets);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Ticket>> GetTicket(string id)
		{
			var ticket = await _ticketService.GetTicketByIdAsync(id);
			if (ticket == null)
			{
				return NotFound();
			}
			return Ok(ticket);
		}

		[HttpPost]
		public async Task<ActionResult<Ticket>> BookTicket(Ticket ticket)
		{
			var newTicket = await _ticketService.BookTicketAsync(ticket);
			return CreatedAtAction(nameof(GetTicket), new { id = newTicket.Id }, newTicket);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateTicket(string id, Ticket ticket)
		{
			var updated = await _ticketService.UpdateTicketAsync(id, ticket);
			if (!updated)
			{
				return NotFound();
			}
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> CancelTicket(string id)
		{
			var canceled = await _ticketService.CancelTicketAsync(id);
			if (!canceled)
			{
				return NotFound();
			}
			return NoContent();
		}
	}
}
