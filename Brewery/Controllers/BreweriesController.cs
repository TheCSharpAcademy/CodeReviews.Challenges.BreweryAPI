using BreweryApi.Models;
using BreweryApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BreweryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreweriesController : ControllerBase
    {
        private BreweryService _breweryService;

        public BreweriesController( BreweryService breweryService )
        {
            _breweryService = breweryService;
        }

        // GET: api/Breweries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brewery>>> GetBrewery()
        {
            return _breweryService.GetBreweries();
        }

        // GET: api/Breweries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Brewery>> GetBrewery(int id)
        {
            var brewery = _breweryService.GetBrewery(id);

            if (brewery == null)
            {
                return NotFound();
            }

            return brewery;
        }

        // PUT: api/Breweries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrewery(int id, Brewery brewery)
        {
            if (id != brewery.Id)
            {
                return BadRequest();
            }

            _breweryService.UpdateBrewery(brewery);

            try
            {
                _breweryService.SaveDb();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_breweryService.BreweryExsists(id))
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

        // POST: api/Breweries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Brewery>> PostBrewery(Brewery brewery)
        {
            _breweryService.InsertBrewery(brewery);

            return CreatedAtAction("GetBrewery", new { id = brewery.Id }, brewery);
        }

        // DELETE: api/Breweries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrewery(int id)
        {
            var brewery = _breweryService.GetBrewery(id);

            if (brewery == null)
            {
                return NotFound();
            }

            _breweryService.DeleteBrewery(brewery);

            return NoContent();
        }
    }
}
