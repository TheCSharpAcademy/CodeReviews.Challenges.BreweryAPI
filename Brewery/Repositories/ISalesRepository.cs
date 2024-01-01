using BreweryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BreweryApi.Repositories
{
    public interface ISalesRepository
    {
        IEnumerable<Sales> getSales();
        Sales getSaleByID( int id );
        Task InsertSale( Sales sales );
        void DeleteSale( Sales sales );
        void UpdateSale( Sales sales );
        void Save();
        void SaveAsync();
        Boolean SaleExists( int id );
        DbSet<Sales> GetAll();
    }
}
