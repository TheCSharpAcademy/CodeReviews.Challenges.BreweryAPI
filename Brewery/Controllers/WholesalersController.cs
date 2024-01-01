using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BreweryApi.Models;
using Newtonsoft.Json;
using BreweryApi.Models.DTOs;
using BreweryApi.Services;

namespace BreweryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WholesalersController : ControllerBase
    {
        private readonly WholesalerService _wholesalerService;

        public WholesalersController( WholesalerService wholesalerService )
        {
            _wholesalerService = wholesalerService;
        }

        // GET: api/Wholesalers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WholesalerDTO>>> GetWholesalers()
        {
            List<Wholesaler> wholesalers = _wholesalerService.GetWholesalers();

            return _wholesalerService.GetWholesalerDTOs(wholesalers);            
        }

        // GET: api/Wholesalers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Wholesaler>> GetWholesaler(int id)
        {
            var wholesaler = _wholesalerService.GetWholesaler(id);

            if (wholesaler == null)
            {
                return NotFound();
            }

            return wholesaler;
        }

        [HttpGet("/api/Sale/beer={beerId}&quantity={quantity}&seller={wholesalerId}")]
        public async Task<ActionResult<string>> GetQuote(int wholesalerId, int beerId, int quantity)
        {
            (bool valid, string message) quote = _wholesalerService.CalculateQuote(wholesalerId, beerId, quantity);

            if (!quote.valid)
            {
                return BadRequest(quote.message);
            }

            var messageJson = JsonConvert.SerializeObject(quote.message);

            return Content(messageJson, "application/json");

        }

        // PUT: api/Wholesalers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWholesaler(int id, Wholesaler wholesaler)
        {
            if (id != wholesaler.Id)
            {
                return BadRequest();
            }

            await _wholesalerService.UpdateWholesaler(wholesaler);

            try
            {
                _wholesalerService.SaveDb();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_wholesalerService.WholesalerExists(id))
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

        // POST: api/Wholesalers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Wholesaler>> PostWholesaler(Wholesaler wholesaler)
        {
            await _wholesalerService.InsertWholesaler(wholesaler);

            return CreatedAtAction("GetWholesaler", new { id = wholesaler.Id }, wholesaler);
        }

        // DELETE: api/Wholesalers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWholesaler(int id)
        {
            var wholesaler = _wholesalerService.GetWholesaler(id);
            if (wholesaler == null)
            {
                return NotFound();
            }

            _wholesalerService.DeleteWholesaler(wholesaler);

            return NoContent();
        }
    }
}
