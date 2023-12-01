using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Models;

namespace CannabisPlantations.WebApi.Data.Repositories
{
    public class CustomerTastingRepository : Repository<CustomerTastings>, ICustomerTastingRepository
    {
        private readonly ApplicationDbContext _db;
        public CustomerTastingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<Customer?> GetCustomers(int tastingId)
        {
            return _db.CustomerTastings
                .Where(ct => ct.TastingId == tastingId)
                .Select(ct => _db.Customers.FirstOrDefault(c => c.Id == ct.CustomerId));
        }

        public IEnumerable<Tasting?> GetTastings(int customerId)
        {
            return _db.CustomerTastings
                .Where(ct => ct.CustomerId == customerId)
                .Select(ct => _db.Tastings.FirstOrDefault(t => t.Id == ct.TastingId));
        }
    }
}
