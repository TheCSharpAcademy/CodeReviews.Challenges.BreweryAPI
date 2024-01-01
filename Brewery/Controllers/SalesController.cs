using Microsoft.AspNetCore.Mvc;
using BreweryApi.Models;
using BreweryApi.Models.DTOs;
using BreweryApi.Services;

namespace BreweryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly SalesService _salesService;

        public SalesController( SalesService salesService )
        {
            _salesService = salesService;
        }

        // GET: api/Sales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleDTO>>> GetSales()
        {
            List<Sales> sales = _salesService.GetSales();

            return _salesService.GetSalesDTO(sales);
        }

        // GET: api/Sales/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SaleDTO>> GetSales( int id )
        {
            var sale = _salesService.GetSale(id);

            if (sale == null)
            {
                return NotFound();
            }

            return _salesService.GetSaleDTO(sale);
        }

        // POST: api/Sales
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sales>> PostSales( Sales sales )
        {

            (Wholesaler wholesaler, Brewery brewery, Beer beer) saleInformation = _salesService.GetSalesInformation(sales);

            if (saleInformation.brewery == null || saleInformation.beer == null)
            {
                return BadRequest("Brewery or beer don't exist or weren't informed");
            }

            if (sales.Quantity <= 0)
            {
                return BadRequest("You can't make a sale without informing the quantity");
            }

            if (saleInformation.wholesaler == null)
            {
                return BadRequest("Wholesaler not informed or dosn't exist. Please check again");
            }

            if (!_salesService.CanWholesalerSellBeer(saleInformation.wholesaler, sales))
            {
                return BadRequest("Wholesaler can't buy this beer");
            }

            if (!_salesService.WholesalerHasSpaceInStock(saleInformation.wholesaler, sales))
            {
                return BadRequest("The current sale exceeds the stock limit of the wholesaler");
            }

            await _salesService.InsertSale(saleInformation.wholesaler, sales);

            return CreatedAtAction("GetSales", new { id = sales.Id }, sales);
        }


        // DELETE: api/Sales/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSales( int id )
        {
            var sales = _salesService.GetSale(id);
            if (sales == null)
            {
                return NotFound();
            }

            _salesService.DeleteSale(sales);

            return NoContent();
        }
    }
}
