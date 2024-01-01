using BreweryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BreweryApi.Repositories
{
    public class SalesRepository : ISalesRepository
    {

        private BreweryContext _context;

        public SalesRepository(BreweryContext context)
        {
            _context = context;
        }

        public void DeleteSale( Sales sale )
        {
            _context.Sales.Remove(sale);
            SaveAsync();
        }

        public DbSet<Sales> GetAll()
        {
            return _context.Sales;
        }

        public Sales getSaleByID( int id )
        {
            return _context.Sales.Find(id);
        }

        public IEnumerable<Sales> getSales()
        {
            return _context.Sales.ToList();
        }

        public async Task InsertSale( Sales sale )
        {
            await _context.Sales.AddAsync(sale);
            await _context.SaveChangesAsync();
        }

        public bool SaleExists( int id )
        {
            return _context.Sales.Any(e => e.Id == id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void SaveAsync()
        {
            _context.SaveChangesAsync();
        }

        public void UpdateSale( Sales sale )
        {
            _context.Entry(sale).State = EntityState.Modified;
        }
    }
}
