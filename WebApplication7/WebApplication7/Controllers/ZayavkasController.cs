using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Models;

[ApiController]
[Route("api/[controller]")]
public class ZayavkasController : ControllerBase
{
	private readonly PizzaMozzarellaContext _context;

	public ZayavkasController(PizzaMozzarellaContext context)
	{
		_context = context;
	}

	[HttpGet]
	public async Task<IEnumerable<Zayavka>> GetZayavkas()
	{
		return await _context.Zayavkas.ToListAsync();
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<Zayavka>> GetZayavka(int id)
	{
		var zayavka = await _context.Zayavkas.FindAsync(id);

		if (zayavka == null)
		{
			return NotFound();
		}

		return zayavka;
	}

	[HttpPost]
	public async Task<ActionResult<Zayavka>> PostZayavka(Zayavka zayavka)
	{
		_context.Zayavkas.Add(zayavka);
		await _context.SaveChangesAsync();

		return CreatedAtAction("GetZayavka", new { id = zayavka.Id }, zayavka);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> PutZayavka(int id, Zayavka zayavka)
	{
		if (id != zayavka.Id)
		{
			return BadRequest();
		}

		_context.Entry(zayavka).State = EntityState.Modified;

		try
		{
			await _context.SaveChangesAsync();
		}
		catch (DbUpdateConcurrencyException)
		{
			if (!ZayavkaExists(id))
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
	public async Task<IActionResult> DeleteZayavka(int id)
	{
		var zayavka = await _context.Zayavkas.FindAsync(id);
		if (zayavka == null)
		{
			return NotFound();
		}

		_context.Zayavkas.Remove(zayavka);
		await _context.SaveChangesAsync();

		return NoContent();
	}

	[HttpGet("search/{number}")]
	public async Task<ActionResult<IEnumerable<Zayavka>>> GetZayavkasByNumber(string number)
	{
		var zayavkas = await _context.Zayavkas.Where(z => z.Number == number).ToListAsync();

		if (zayavkas == null || !zayavkas.Any())
		{
			return NotFound();
		}

		return zayavkas;
	}
	private bool ZayavkaExists(int id)
	{
		return _context.Zayavkas.Any(e => e.Id == id);
	}
}
