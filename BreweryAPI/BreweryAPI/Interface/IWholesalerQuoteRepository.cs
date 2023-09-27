using BreweryAPI.Models;

namespace BreweryAPI.Interface
{
    public interface IWholesalerQuoteRepository
    {
        ICollection<WholesalerQuoteModel> GetWholesalerQuotes();
        WholesalerQuoteModel GetWholesalerQuote(int id);
        bool CreateWholesalerQuote(WholesalerQuoteModel wholesalerQuote);
        bool UpdateWholesalerQuote(WholesalerQuoteModel wholesalerQuote);
        bool DeleteWholesalerQuote(WholesalerQuoteModel wholesalerQuote);
        bool WholesalerQuoteExists(int id);
        bool Save();
    }
}
