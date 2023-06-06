using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ClientController : ControllerBase
	{
		private readonly PizzaMozzarellaContext _context;

		public ClientController(PizzaMozzarellaContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Client>>> GetClients()
		{
			return await _context.Clients.ToListAsync();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Client>> GetClient(int id)
		{
			var client = await _context.Clients.FindAsync(id);

			if (client == null)
			{
				return NotFound();
			}

			return client;
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateClient(int id, Client client)
		{
			if (id != client.ClientId)
			{
				return BadRequest();
			}

			_context.Entry(client).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ClientExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		[HttpPost]
		public async Task<ActionResult<Client>> CreateClient(Client client)
		{
			_context.Clients.Add(client);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetClient", new { id = client.ClientId }, client);
		}

		[HttpGet("FindClient")]
		public async Task<ActionResult<Client>> FindClient(string phoneNumber, string password)
		{
			var client = await _context.Clients.FirstOrDefaultAsync(c => c.PhoneNumber == phoneNumber && c.Password == password);

			if (client == null)
			{
				return NotFound();
			}

			return client;
		}

		[HttpPost("CreateClientFromApp")]
		public async Task<ActionResult<int>> CreateClientFromApp(WebApplication7.Models.Client client)
		{
			var newClient = new Client
			{
				LastName = client.LastName,
				FirstName = client.FirstName,
				Password = client.Password,
				DateOfBirth = client.DateOfBirth, // Convert to appropriate type if needed.
				PhoneNumber = client.PhoneNumber
			};

			_context.Clients.Add(newClient);
			await _context.SaveChangesAsync();

			return newClient.ClientId;
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteClient(int id)
		{
			var client = await _context.Clients.FindAsync(id);
			if (client == null)
			{
				return NotFound();
			}

			_context.Clients.Remove(client);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool ClientExists(int id)
		{
			return _context.Clients.Any(e => e.ClientId == id);
		}
	}
}
