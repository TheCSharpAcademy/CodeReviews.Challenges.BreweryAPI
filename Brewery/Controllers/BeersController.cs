using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BreweryApi.Models;
using BreweryApi.Models.DTOs;
using BreweryApi.Services;

namespace BreweryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeersController : ControllerBase
    {
        private readonly BeerService _beerService;

        public BeersController(BeerService beerService)
        {
            _beerService = beerService;
        }

        // GET: api/Beers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BeerDTO>>> GetBeer()
        {
            return _beerService.GetBeers();
        }

        // GET: api/Beers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BeerDTO>> GetBeer(int id)
        {
            var result = _beerService.GetBeerDTO(id);

            if ( result == null) { return NotFound(); }

            return result;

        }

        // PUT: api/Beers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBeer(int id, Beer beer)
        {
            if (id != beer.Id)
            {
                return BadRequest();
            }

            _beerService.UpdateBeer(beer);

            try
            {
                _beerService.SaveDb();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_beerService.BeerExsists(id))
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

        // POST: api/Beers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Beer>> PostBeer(Beer beer)
        {
            _beerService.InsertBeer(beer);

            return CreatedAtAction(nameof(GetBeer), new { id = beer.Id }, beer);
        }

        // DELETE: api/Beers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBeer(int id)
        {
            var beer = _beerService.GetBeer(id);

            if (beer == null)
            {
                return NotFound();
            }

            _beerService.DeleteBeer(beer);

            return NoContent();
        }
    }
}
