using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Models;

[ApiController]
[Route("api/[controller]")]
public class PositionsController : ControllerBase
{
	private readonly PizzaMozzarellaContext _context;

	public PositionsController(PizzaMozzarellaContext context)
	{
		_context = context;
	}

	[HttpGet]
	public async Task<IEnumerable<Position>> GetPositions()
	{
		return await _context.Positions.ToListAsync();
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<Position>> GetPosition(int id)
	{
		var position = await _context.Positions.FindAsync(id);

		if (position == null)
		{
			return NotFound();
		}

		return position;
	}

	[HttpPost]
	public async Task<ActionResult<Position>> PostPosition(Position position)
	{
		_context.Positions.Add(position);
		await _context.SaveChangesAsync();

		return CreatedAtAction("GetPosition", new { id = position.PositionId }, position);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> PutPosition(int id, Position position)
	{
		if (id != position.PositionId)
		{
			return BadRequest();
		}

		_context.Entry(position).State = EntityState.Modified;

		try
		{
			await _context.SaveChangesAsync();
		}
		catch (DbUpdateConcurrencyException)
		{
			if (!PositionExists(id))
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

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeletePosition(int id)
	{
		var position = await _context.Positions.FindAsync(id);
		if (position == null)
		{
			return NotFound();
		}

		_context.Positions.Remove(position);
		await _context.SaveChangesAsync();

		return NoContent();
	}

	private bool PositionExists(int id)
	{
		return _context.Positions.Any(e => e.PositionId == id);
	}
}
