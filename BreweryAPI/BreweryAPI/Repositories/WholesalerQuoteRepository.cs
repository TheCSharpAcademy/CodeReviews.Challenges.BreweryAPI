using BreweryAPI.Interface;
using BreweryAPI.Models;

namespace BreweryAPI.Repositories
{
    public class WholesalerQuoteRepository : IWholesalerQuoteRepository
    {
        private readonly Context _context;
        public WholesalerQuoteRepository(Context context)
        {
            _context = context;
        }

        public bool CreateWholesalerQuote(WholesalerQuoteModel wholesalerQuote)
        {
            _context.Add(wholesalerQuote);
            return Save();
        }

        public bool DeleteWholesalerQuote(WholesalerQuoteModel wholesalerQuote)
        {
            _context.Remove(wholesalerQuote);
            return Save();
        }

        public bool UpdateWholesalerQuote(WholesalerQuoteModel wholesalerQuote)
        {
            _context.Update(wholesalerQuote);
            return Save();
        }

        public WholesalerQuoteModel GetWholesalerQuote(int id)
        {
            return _context.WholesalerQuotes.Where(wq => wq.QuoteId == id).FirstOrDefault();
        }

        public ICollection<WholesalerQuoteModel> GetWholesalerQuotes()
        {
            return _context.WholesalerQuotes.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool WholesalerQuoteExists(int id)
        {
            return _context.WholesalerQuotes.Any(wq => wq.QuoteId == id);
        }
    }
}
