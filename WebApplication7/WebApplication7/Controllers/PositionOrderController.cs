using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Models;

[ApiController]
[Route("api/[controller]")]
public class PositionOrdersController : ControllerBase
{
	private readonly PizzaMozzarellaContext _context;

	public PositionOrdersController(PizzaMozzarellaContext context)
	{
		_context = context;
	}

	[HttpGet]
	public async Task<IEnumerable<PositionOrder>> GetPositionOrders()
	{
		return await _context.PositionOrders.ToListAsync();
	}

	[HttpGet("{positionId}/{orderId}")]
	public async Task<ActionResult<PositionOrder>> GetPositionOrder(int positionId, int orderId)
	{
		var positionOrder = await _context.PositionOrders.FindAsync(positionId, orderId);

		if (positionOrder == null)
		{
			return NotFound();
		}

		return positionOrder;
	}

	[HttpPost]
	public async Task<ActionResult<PositionOrder>> PostPositionOrder(PositionOrder positionOrder)
	{
		_context.PositionOrders.Add(positionOrder);
		await _context.SaveChangesAsync();

		return CreatedAtAction("GetPositionOrder", new { positionId = positionOrder.PositionId, orderId = positionOrder.OrderId }, positionOrder);
	}

	[HttpPut("{positionId}/{orderId}")]
	public async Task<IActionResult> PutPositionOrder(int positionId, int orderId, PositionOrder positionOrder)
	{
		if (positionId != positionOrder.PositionId || orderId != positionOrder.OrderId)
		{
			return BadRequest();
		}

		_context.Entry(positionOrder).State = EntityState.Modified;

		try
		{
			await _context.SaveChangesAsync();
		}
		catch (DbUpdateConcurrencyException)
		{
			if (!PositionOrderExists(positionId, orderId))
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

	[HttpDelete("{positionId}/{orderId}")]
	public async Task<IActionResult> DeletePositionOrder(int positionId, int orderId)
	{
		var positionOrder = await _context.PositionOrders.FindAsync(positionId, orderId);
		if (positionOrder == null)
		{
			return NotFound();
		}

		_context.PositionOrders.Remove(positionOrder);
		await _context.SaveChangesAsync();

		return NoContent();
	}

	private bool PositionOrderExists(int positionId, int orderId)
	{
		return _context.PositionOrders.Any(e => e.PositionId == positionId && e.OrderId == orderId);
	}
}
