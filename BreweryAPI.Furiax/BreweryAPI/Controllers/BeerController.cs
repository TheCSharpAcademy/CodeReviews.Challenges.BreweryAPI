using BreweryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BreweryAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BeerController : ControllerBase
	{
		private readonly BreweryContext _context;

		public BeerController(BreweryContext context)
		{
			_context = context;
		}

		// GET: api/BeersByBrewer
		[HttpGet("BeersByBrewer/{brewerId}")]
		public async Task<ActionResult<IEnumerable<BeerModel>>> GetBeersByBrewer(int brewerId)
		{
			if (_context.Beers == null)
			{
				return NotFound();
			}
			var beersByBrewer = await _context.Beers
				.Where(beer => beer.BrewerId == brewerId)
				.Include(b => b.Brewer)
				.ToListAsync();

			if (beersByBrewer == null || beersByBrewer.Count == 0)
			{
				return NotFound();
			}

			return beersByBrewer;
		}

		// GET: api/Beer
		[HttpGet]
		public async Task<ActionResult<IEnumerable<BeerModel>>> GetBeers()
		{
			if (_context.Beers == null)
			{
				return NotFound();
			}
			return await _context.Beers.Include(b => b.Brewer).ToListAsync();

		}



		// GET: api/Beer/5
		[HttpGet("{id}")]
		public async Task<ActionResult<BeerModel>> GetBeerModel(int id)
		{
			if (_context.Beers == null)
			{
				return NotFound();
			}
			var beerModel = await _context.Beers.Include(b => b.Brewer).FirstOrDefaultAsync(b => b.BeerId == id);

			if (beerModel == null)
			{
				return NotFound();
			}

			return beerModel;
		}

		// PUT: api/Beer/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutBeerModel(int id, BeerModel updatedBeerModel)
		{
			if (id != updatedBeerModel.BeerId)
			{
				return BadRequest();
			}

			var existingBrewery = await _context.Breweries.FindAsync(updatedBeerModel.BrewerId);
			if (existingBrewery == null)
			{
				return NotFound("Brewery not found.");
			}

			updatedBeerModel.Brewer = existingBrewery;

			_context.Entry(updatedBeerModel).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!BeerModelExists(id))
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

		// POST: api/Beer
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<BeerModel>> PostBeerModel(BeerModel beerModel)
		{
			if (_context.Beers == null || _context.Breweries == null)
			{
				return Problem("Entity set 'BreweryContext.Beers' or 'BreweryContext.Breweries' is null.");
			}

			var existingBrewery = await _context.Breweries.FindAsync(beerModel.BrewerId);
			if (existingBrewery == null)
			{
				return NotFound("Brewery not found.");
			}

			beerModel.Brewer = existingBrewery;

			_context.Beers.Add(beerModel);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetBeerModel", new { id = beerModel.BeerId }, beerModel);
		}

		// DELETE: api/Beer/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteBeerModel(int id)
		{
			if (_context.Beers == null)
			{
				return NotFound();
			}
			var beerModel = await _context.Beers.FindAsync(id);
			if (beerModel == null)
			{
				return NotFound();
			}

			_context.Beers.Remove(beerModel);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool BeerModelExists(int id)
		{
			return (_context.Beers?.Any(e => e.BeerId == id)).GetValueOrDefault();
		}
	}
}
