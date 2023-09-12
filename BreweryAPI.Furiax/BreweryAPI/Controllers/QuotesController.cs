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
		public async Task<ActionResult<OrderModel>> RequestQuote(QuoteModel quoteModel)
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

				var output = new List<OrderModel>();
				decimal totalPrice = 0;
				int totalQuantity = 0;
				string summary = "";
				int discount = 0;

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
					var beerInfo = await _context.Beers.FindAsync(item.BeerId);
					totalPrice += beerInfo.Price * item.Quantity;
					totalQuantity += item.Quantity;
					summary += $"{beerInfo.Name} * {item.Quantity} = {beerInfo.Price * item.Quantity}\n";
				}

				if (totalQuantity >= 10)
				{
					if (totalQuantity >= 20) 
					{ 
						discount = 20;
						totalPrice -= (totalPrice * discount) / 100;
					}
					else
					{
						discount = 10;
						totalPrice -= (totalPrice * discount) / 100;
					}
					summary += $"Discount: {discount}% \n";
				}
				summary += new string('-',20);
				summary += $"\nTotal: {totalPrice}";
				output.Add(new OrderModel { Price = totalPrice, Discount = discount, Quote = quoteModel, Summary = summary});
				

				return CreatedAtAction(nameof(RequestQuote), output);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
