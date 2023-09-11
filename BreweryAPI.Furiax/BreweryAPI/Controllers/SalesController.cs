using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BreweryAPI;
using BreweryAPI.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BreweryAPI.Controllers
{
    [Route("Sales")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly BreweryContext _context;

        public SalesController(BreweryContext context)
        {
            _context = context;
        }

        // GET: api/Sales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleModel>>> GetSales()
        {
          if (_context.Sales == null)
          {
              return NotFound();
          }
			return await _context.Sales
                .Include(w => w.Wholesaler)
                .Include(b => b.Beer)
                .Include(br => br.Beer.Brewer)
                .ToListAsync();
        }

        // GET: api/Sales/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SaleModel>> GetSaleModel(int id)
        {
          if (_context.Sales == null)
          {
              return NotFound();
          }
            var saleModel = await _context.Sales
				.Include(w => w.Wholesaler)
				.Include(b => b.Beer)
				.Include(br => br.Beer.Brewer)
                .FirstOrDefaultAsync(s => s.SaleId == id);

            if (saleModel == null)
            {
                return NotFound();
            }

            return saleModel;
        }

        // PUT: api/Sales/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSaleModel(int id, SaleModel updatedSaleModel)
        {
            if (id != updatedSaleModel.SaleId)
            {
                return BadRequest();
            }

            _context.Entry(updatedSaleModel).State = EntityState.Modified;

            var existingWholesaler = await _context.Wholesalers.FindAsync(updatedSaleModel.WholesalerId);
            if (existingWholesaler == null)
            {
                return NotFound("The wholesaler found with that id.");
            }
            updatedSaleModel.Wholesaler = existingWholesaler;

            var existingBeer = await _context.Beers
			                   .Include(br => br.Brewer)
				               .FirstOrDefaultAsync(b => b.BeerId == updatedSaleModel.BeerId);
			if (existingBeer == null)
            {
                return NotFound("A beer with that id doesn't exist.");
            }
            updatedSaleModel.Beer = existingBeer;

            _context.Entry(updatedSaleModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleModelExists(id))
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

        // POST: api/Sales
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SaleModel>> PostSaleModel(SaleModel saleModel)
        {
          if (_context.Sales == null)
          {
              return Problem("Entity set 'BreweryContext.Sales'  is null.");
          }
          var existingWholesaler = await _context.Wholesalers.FindAsync(saleModel.WholesalerId);
          if (existingWholesaler == null)
           {
               return NotFound("No Wholesaler found with that id.");
           }
           saleModel.Wholesaler = existingWholesaler;

            var existingBeer = await _context.Beers
                .Include(br => br.Brewer)
                .FirstOrDefaultAsync(b => b.BeerId == saleModel.BeerId);
            if (existingBeer == null)
            {
                return NotFound("No beer found with that id.");
            }
            saleModel.Beer = existingBeer;

           _context.Sales.Add(saleModel);
           await _context.SaveChangesAsync();

           return CreatedAtAction("GetSaleModel", new { id = saleModel.SaleId }, saleModel);
        }

// DELETE: api/Sales/5
[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSaleModel(int id)
        {
            if (_context.Sales == null)
            {
                return NotFound();
            }
            var saleModel = await _context.Sales.FindAsync(id);
            if (saleModel == null)
            {
                return NotFound();
            }

            _context.Sales.Remove(saleModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SaleModelExists(int id)
        {
            return (_context.Sales?.Any(e => e.SaleId == id)).GetValueOrDefault();
        }
    }
}
