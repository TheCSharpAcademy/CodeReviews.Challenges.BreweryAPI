using BreweryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BreweryAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class QuotesController : ControllerBase
	{
		private readonly BreweryContext _context;

		public QuotesController(BreweryContext context)
		{
			_context = context;
		}

		// POST: api/Quotes
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<QuoteModel>> RequestQuote(QuoteModel quoteModel)
		{

			try
			{
				//check if model isnt empty
				if (quoteModel == null)
				{
					return BadRequest("Order cannot be empty");
				}

				//check if the wholesaler exist
				var wholesalerExisting = await _context.Wholesalers.FindAsync(quoteModel.WholeSalerId);
				if (wholesalerExisting == null)
				{
					return NotFound("Wholesaler doesn't exist");
				}

				//check for duplicates
				if(quoteModel.Orders.GroupBy(ord => ord.BeerId).Any(cnt => cnt.Count() >1))
				{
					return BadRequest("There can't be any duplicates in the order");
				}

				
				foreach (var item in quoteModel.Orders)
				{
					var isBeerBeingSold = await _context.Sales.FirstOrDefaultAsync(s => s.BeerId == item.BeerId && s.WholesalerId == quoteModel.WholeSalerId);
					if (isBeerBeingSold == null)
					{
						return NotFound("The wholesaler does not sell all items from your list");
					}

					var isStockHighEnough = await _context.Sales.FirstOrDefaultAsync(s => s.BeerId == item.BeerId && s.WholesalerId == quoteModel.WholeSalerId && s.Quantity > item.Quantity);
					if (isStockHighEnough == null)
					{
						return NotFound("The wholesaler does not have enough stock to fullfill your order");
					}

				}

				
				_context.Quotes.Add(quoteModel);
				await _context.SaveChangesAsync();

				return CreatedAtAction("GetQuoteModel", quoteModel);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
