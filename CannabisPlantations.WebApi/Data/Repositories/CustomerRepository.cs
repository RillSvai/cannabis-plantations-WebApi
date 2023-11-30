using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CannabisPlantations.WebApi.Data.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly ApplicationDbContext _db;
        public CustomerRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public override void Delete(Customer entity)
        {
            _db.RemoveRange(_db.CustomerTastings.Where(ct => ct.CustomerId == entity.Id));
            _db.RemoveRange(_db.Feedbacks.Where(f => f.CustomerId == entity.Id));
            base.Delete(entity);
        }
        public override void DeleteRange(IEnumerable<Customer> entities)
        {
            IEnumerable<CustomerTastings> customerTastings = _db.CustomerTastings.Where(ct => entities.Any(c => c.Id == ct.CustomerId));
            IEnumerable<Feedback> feedbacks = _db.Feedbacks.Where(f => entities.Any(c => c.Id == f.CustomerId));
            _db.CustomerTastings.RemoveRange(customerTastings);
            _db.Feedbacks.RemoveRange(feedbacks);
            base.DeleteRange(entities);
        }

        public async Task<IEnumerable<Agronomist?>> GetAgronomistsByAtLeastOneProductTasting(int customerId, DateTime since, DateTime until)
        {
            List<int> agronomistsAtLeastOneOrder = await _db.Orders
                .Where(o => o.CustomerId == customerId && o.Date <= until && o.Date >= since)
                .Select(o => o.AgronomistId)
                .Distinct()
                .ToListAsync();
            List<int> agronomistsAtLeastOneTasting = await _db.CustomerTastings
                .Where(ct => ct.CustomerId == customerId)
                .Join(_db.Tastings, ct => ct.TastingId, t => t.Id, (ct, t) => new
                {
                    AgronomistId = t.AgronomistId,
                    Date = t.Date
                })
                .Where(ct_t => ct_t.Date >= since && ct_t.Date <= until)
                .Select(ct_t => ct_t.AgronomistId)
                .Distinct()
                .ToListAsync();
            return  agronomistsAtLeastOneOrder
                .Intersect(agronomistsAtLeastOneTasting)
                .Select(i => _db.Agronomists
                .FirstOrDefault(a => a.Id == i));
        }

        public IEnumerable<Agronomist?> GetAgronomistsByMinTastings(int customerId, int tastingsNumber, DateTime since, DateTime until)
        {
            return _db.CustomerTastings
                .Where(ct => ct.CustomerId == customerId)
                .Join(_db.Tastings.Where(t => t.Date <= until && t.Date >= since), ct => ct.TastingId, t => t.Id, (ct, t) => new
                {
                    AgronomistId = t.AgronomistId
                })
                .GroupBy(a => a.AgronomistId)
                .Where(g => g.Count() >= tastingsNumber)
                .Select(g => _db.Agronomists.FirstOrDefault(a => a.Id == g.Key));
                
        }

        public IEnumerable<Tasting?> GetCommonTastingsBetweenCustomerAgronomist(int customerId, int agronomistId, DateTime since, DateTime until)
        {
            return _db.CustomerTastings
                .Where(ct => ct.CustomerId == customerId)
                .Join(_db.Tastings
                .Where(t => t.Date <= until && t.Date >= since && t.AgronomistId == agronomistId), ct => ct.TastingId, t => t.Id, (ct, t) => t.Id)
                .Select(i => _db.Tastings.FirstOrDefault(t => t.Id == i));
           
                
        }

        public IEnumerable<Product?> GetPurchasedProducts(int customerId, DateTime since, DateTime until)
        {
            return _db.OrderDetails
                .Join(_db.Orders, od => od.OrderId, o => o.Id, (od, o) => new
                {
                    ProductId = od.ProductId,
                    Date = o.Date,
                    CustomerId = o.CustomerId
                })
                .Where(od_o => od_o.CustomerId == customerId && od_o.Date >= since && od_o.Date <= until)
                .GroupBy(od_o => od_o.ProductId)
                .Select(od_o => _db.Products.FirstOrDefault(p => p.Id == od_o.Key));
        }
    }
}
